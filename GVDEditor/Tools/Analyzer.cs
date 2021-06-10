﻿using GVDEditor.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using GVDEditor.Forms;

namespace GVDEditor.Tools
{
    internal enum FixType
    {
        /// <summary>
        ///     Program problem opravi uplne sam automaticky
        /// </summary>
        AUTO,

        /// <summary>
        ///     Pouzivatel musi vybrat jednu z ponukanych moznosti, aby chybu opravil
        /// </summary>
        SEMI_AUTO,

        /// <summary>
        ///     Pouzivatel musi chybu opravit sam a program mu len ukaze, kde ma chybu opravit
        /// </summary>
        MANUAL 
    }

    internal enum ProblemType
    {
        /// <summary>
        ///     Len informacia pre pouzivatela. Grafikon je uplne funkcny.
        /// </summary>
        HINT,

        /// <summary>
        ///     Grafikon nemusi fungovat uplne spravne, ale je spustitelny
        /// </summary>
        WARNING,

        /// <summary>
        ///     Zavazna chyba v grafikone. INISS pravdepodobne nespusti tento grafikon.
        /// </summary>
        ERROR
    }

    internal enum FixResult
    {
        /// <summary>
        ///     Ak bola chyba opravena
        /// </summary>
        DONE,

        /// <summary>
        ///     Ak pouzivatel chybu neopravil
        /// </summary>
        NOT_SOLVED,

        /// <summary>
        ///     Ak pocas opravy doslo k chybe
        /// </summary>
        ERROR
    }

    internal interface IProblem
    {
        public string Text { get; }

        public string Solution { get; }

        public ProblemType ProblemType { get; }

        public FixType FixType { get; }

        public FixResult FixProblem();
    }

    internal static class Analyzer
    {
        public static List<IProblem> FindProblems(BackgroundWorker bw, GVDDirectory gvd)
        {
            List<IProblem> problems = new List<IProblem>();

            //1. Check GVD validity
            var now = DateTime.Now;
            if (gvd.GVD.EndValidData < now)
            {
                var problem = new GVDOutOfValidity(gvd);
                problems.Add(problem);
            }
            bw.ReportProgress(5);

            //2. Check Empty TabTabs
            foreach (var tab in GlobData.TabTabs)
            {
                if (string.IsNullOrEmpty(tab.Text))
                {
                    var problem = new EmptyTabTab(tab);
                    problems.Add(problem);
                }
            }
            bw.ReportProgress(10);

            //3. Check using Catalog tables in TPhysic and in TableTextRealization AND Segments
            foreach (var catalog in GlobData.TableCatalogs)
            {
                if (catalog.Segments.Count == 0)
                {
                    var problem = new TableWithoutSegments(catalog);
                    problems.Add(problem);
                }

                bool unused = true;
                foreach (var physical in GlobData.TablePhysicals)
                {
                    if (physical.TableCatalog == catalog)
                    {
                        unused = false;
                        break;
                    }
                }

                if (!unused)
                {
                    continue;
                }

                foreach (var tt in GlobData.TableTexts)
                {
                    foreach (var realization in tt.Realizations)
                    {
                        if (realization.Catalog == catalog)
                        {
                            unused = false;
                        }
                    }
                }

                if (unused)
                {
                    var problem = new UnusedTable(catalog);
                    problems.Add(problem);
                }
            }
            bw.ReportProgress(25);

            //4. Check using Physic tables in Tlogical
            foreach (var physical in GlobData.TablePhysicals)
            {
                bool unused = true;
                foreach (var logical in GlobData.TableLogicals)
                {
                    foreach (var rec in logical.Records)
                    {
                        foreach (var position in rec.Positions)
                        {
                            if (position.TablePhysical == physical)
                            {
                                unused = false;
                                break;
                            }
                        }
                        if (!unused) break;
                    }
                    if (!unused) break;
                }

                if (unused)
                {
                    var problem = new UnusedTable(physical);
                    problems.Add(problem);
                }
            }
            bw.ReportProgress(50);

            //5. Check using TabTabs
            foreach (var tab in GlobData.TabTabs)
            {
                bool unused = true;
                foreach (var catalog in GlobData.TableCatalogs)
                {
                    foreach (var item in catalog.Items)
                    {
                        if (item.Tab1 == tab || item.Tab2 == tab)
                        {
                            unused = false;
                            break;
                        }
                    }
                    if (!unused) break;
                }

                if (unused)
                {
                    var problem = new UnusedTabTab(tab);
                    problems.Add(problem);
                }
            }
            bw.ReportProgress(75);

            //6. Check TTexts
            for (var i = 0; i < GlobData.TableTexts.Count; i++)
            {
                var tableText = GlobData.TableTexts[i];
                if (tableText.Realizations.Count == 0)
                {
                    var problem = new TableTextWithoutRealization(tableText, gvd, i);
                    problems.Add(problem);
                }

                if (tableText.Trains.Count == 0)
                {
                    var problem = new TableTextWithoutTrains(tableText, gvd, i);
                    problems.Add(problem);
                }
            }

            bw.ReportProgress(100);

            return problems;
        }
    }

    internal class UnusedTable : IProblem
    {
        /// <summary>Initializes a new instance of the <see cref="UnusedTable" /> class.</summary>
        public UnusedTable(ITable table)
        {
            Table = table;
        }

        public string Text
        {
            get
            {
                string tabname = Table switch
                {
                    TableCatalog => "Katalógová",
                    TablePhysical => "Fyzická",
                    TableLogical => "Logická",
                    _ => ""
                };
                return $"{tabname} tabuľa {Table.Key} sa nikde nepoužíva.";
            }
        }

        public string Solution => "Odstrániť tabuľu";

        public ProblemType ProblemType => ProblemType.HINT;

        public FixType FixType => FixType.AUTO;

        public ITable Table { get; }

        public FixResult FixProblem()
        {
            return Table switch
            {
                TableCatalog tc => GlobData.TableCatalogs.Remove(tc) ? FixResult.DONE : FixResult.NOT_SOLVED,
                TablePhysical tb => GlobData.TablePhysicals.Remove(tb) ? FixResult.DONE : FixResult.NOT_SOLVED,
                TableLogical tl => GlobData.TableLogicals.Remove(tl) ? FixResult.DONE : FixResult.NOT_SOLVED,
                _ => FixResult.NOT_SOLVED
            };
        }
    }

    internal class UnusedTabTab : IProblem
    {
        /// <summary>Initializes a new instance of the <see cref="UnusedTabTab" /> class.</summary>
        public UnusedTabTab(TableTabTab table)
        {
            TabTab = table;
        }

        public string Text => $"TabTab {TabTab.Key} sa nikde nepoužíva.";

        public string Solution => "Odstrániť TabTab";

        public ProblemType ProblemType => ProblemType.HINT;

        public FixType FixType => FixType.AUTO;

        public TableTabTab TabTab { get; }

        public FixResult FixProblem()
        {
            return GlobData.TabTabs.Remove(TabTab) ? FixResult.DONE : FixResult.NOT_SOLVED;
        }
    }

    internal class TableWithoutSegments : IProblem
    {
        /// <summary>Initializes a new instance of the <see cref="TableWithoutSegments" /> class.</summary>
        public TableWithoutSegments(TableCatalog table)
        {
            Table = table;
        }

        public string Text => $"Katalógova tabuľa {Table.Key} nemá nastavené žiadne riadky (segmenty).";

        public string Solution => "Upraviť segmenty katalógovej tabule";

        public ProblemType ProblemType => ProblemType.WARNING;

        public FixType FixType => FixType.MANUAL;

        public TableCatalog Table { get; }

        public FixResult FixProblem()
        {
            var form = new FTableCatalog(Table, GlobData.TabTabs);
            form.ShowDialog();

            //Check if the problem was solved
            return Table.Segments.Count == 0 ? FixResult.NOT_SOLVED : FixResult.DONE;
        }
    }

    internal class TableTextWithoutRealization : IProblem
    {
        /// <summary>Initializes a new instance of the <see cref="TableTextWithoutRealization" /> class.</summary>
        public TableTextWithoutRealization(TableText text, GVDDirectory gvd, int row)
        {
            TText = text;
            GVD = gvd;
            Row = row;
        }

        public string Text => $"Table Text je \"{TText.Key}\" nemá žiadnu realizáciu.";

        public string Solution => "Pridať realizácie textu";

        public ProblemType ProblemType => ProblemType.WARNING;

        public FixType FixType => FixType.MANUAL;

        public TableText TText { get; }

        public GVDDirectory GVD { get; }

        public int Row { get; }

        public FixResult FixProblem()
        {
            var form = new FTableText(TText, GlobData.TableCatalogs, GVD.GVD, Row);
            form.ShowDialog();

            //Check if the problem was solved
            return TText.Realizations.Count == 0 ? FixResult.NOT_SOLVED : FixResult.DONE;
        }
    }

    internal class TableTextWithoutTrains : IProblem
    {
        /// <summary>Initializes a new instance of the <see cref="TableTextWithoutTrains" /> class.</summary>
        public TableTextWithoutTrains(TableText text, GVDDirectory gvd, int row)
        {
            TText = text;
            GVD = gvd;
            Row = row;
        }

        public string Text => $"Table Text je \"{TText.Key}\" nemá nastavené žiadne texty vlakov.";

        public string Solution => "Pridať realizácie textu";

        public ProblemType ProblemType => ProblemType.WARNING;

        public FixType FixType => FixType.MANUAL;

        public TableText TText { get; }

        public GVDDirectory GVD { get; }

        public int Row { get; }

        public FixResult FixProblem()
        {
            var form = new FTableText(TText, GlobData.TableCatalogs, GVD.GVD, Row);
            form.ShowDialog();

            //Check if the problem was solved
            return TText.Trains.Count == 0 ? FixResult.NOT_SOLVED : FixResult.DONE;
        }
    }

    internal class EmptyTabTab : IProblem
    {
        /// <summary>Initializes a new instance of the <see cref="EmptyTabTab" /> class.</summary>
        public EmptyTabTab(TableTabTab tabTab)
        {
            TabTab = tabTab;
        }

        public string Text => $"TabTab {TabTab.Key} je prázdny";

        public string Solution => "Upraviť TabTab";

        public ProblemType ProblemType => ProblemType.WARNING;

        public FixType FixType => FixType.MANUAL;

        public TableTabTab TabTab { get; }

        public FixResult FixProblem()
        {
            var form = new FTabTab(TabTab);
            form.ShowDialog();

            //Check if the problem was solved
            return string.IsNullOrEmpty(TabTab.Text) ? FixResult.NOT_SOLVED : FixResult.DONE;
        }
    }

    internal class GVDOutOfValidity : IProblem
    {
        /// <summary>Initializes a new instance of the <see cref="GVDOutOfValidity" /> class.</summary>
        public GVDOutOfValidity(GVDDirectory gvd)
        {
            GVD = gvd;
        }

        public string Text => $"Grafikon {GVD.PeriodFormatted} je po planosti";

        public string Solution => "Zmeniť platnosť grafikonu";

        public ProblemType ProblemType => ProblemType.WARNING;

        public FixType FixType => FixType.MANUAL;

        public GVDDirectory GVD { get; }

        public FixResult FixProblem()
        {
            var form = new FLocalSettings(GVD, 0);
            form.ShowDialog();

            //Check if the problem was solved
            return GVD.GVD.EndValidData < DateTime.Now ? FixResult.NOT_SOLVED : FixResult.DONE;
        }
    }
}
