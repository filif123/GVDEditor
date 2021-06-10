using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GVDEditor.Entities;

namespace GVDEditor.Tools
{
    /// <summary>
    ///     Trieda spracujuca text exportovany z programu ELIS do objektov
    /// </summary>
    public static class ELISParser
    {
        private const string FORMAT_EX = "Chyba v súbore na riadku {0}. ";

        /// <summary>
        ///     Spracuje textovy subor exportovany z programu ELIS do zoznamu vlakov ako objekty typu <see cref="Train"/>.
        /// </summary>
        /// <param name="path">cesta k vygenerovanemu ELIS suboru</param>
        /// <param name="deftrains">zoznam uz definovanych vlakov</param>
        /// <param name="trainTypes">vsetky typy vlakov definovane v stanici</param>
        /// <param name="operators">zoznam vsetkych definovanych dopravcov</param>
        /// <param name="gvd">informacie o aktualnom grafikone</param>
        /// <param name="defaultTrack">kolaj, ktora bude priradena kazdemu vlaku</param>
        /// <param name="omitPassingTrains">ci sa maju preskocit vlaky, ktore su prechadzajuce</param>
        /// <param name="defOperators">ci su v subore definovani dopravcovia ku kazdemu vlaku</param>
        /// <param name="defDaterems">ci su v subore definovane datumove obmedzenia ku kazdemu vlaku</param>
        /// <exception cref="ArgumentException">ak sa zisti neplatny typ vlaku alebo datum</exception>
        /// <exception cref="FormatException">ak sa zistia nedefinovane stanice alebo subor obsahuje neocakavane znaky</exception>
        public static List<Train> ReadELISFile(string path, List<Train> deftrains, List<TrainType> trainTypes, List<Operator> operators, GVDInfo gvd, Track defaultTrack, bool omitPassingTrains = false, bool defOperators = true, bool defDaterems = true)
        {
            if (!File.Exists(path)) return null;

            var wstations = new List<string>();
            var trains = new List<Train>(deftrains);
            int countdef = deftrains.Count;

            using var file = new StreamReader(File.OpenRead(path));
            var riadok = 0;

            while (!file.EndOfStream)
                try
                {
                    //inicializacia vlaku a prveho riadku
                    var firstrow = file.ReadLine();
                    riadok++;
                    if (firstrow == null)
                    {
                        trains.RemoveRange(0, countdef);
                        return trains;
                    }
                    var row = firstrow.Split(' ');
                    var count = row.Length;
                    var train = new Train();

                    //typ vlaku
                    foreach (var type in trainTypes.Where(type => row[0] == type.Key)) train.Type = type;
                    if (train.Type == null) throw new ArgumentException($"Neexistujúci typ vlaku {row[0]}");

                    //cislo vlaku
                    train.Number = row[1];

                    //nazov vlaku
                    var nazov = "";
                    for (var i = 2; i < count - 5; i++) 
                        nazov += row[i] + " ";
                    train.Name = nazov.Trim();

                    //obdobie platnosti
                    train.ZaciatokPlatnosti = Utils.ParseDateAlts(row[count - 3]);
                    train.KoniecPlatnosti = Utils.ParseDateAlts(row[count - 1]);

                    //spracovanie variant
                    train.Variant = -2;

                    //preskocenie
                    file.ReadLine();
                    file.ReadLine();
                    file.ReadLine();
                    riadok += 3;

                    //stanice vlaku
                    Station stanica = null;
                    var thisst = false;
                    while (true)
                    {
                        var rowsts = SplitRow(file.ReadLine());
                        riadok++;
                        var count2 = rowsts.Length;
                        if (rowsts[0].StartsWith("-") || rowsts[0].StartsWith("="))
                        {
                            if (stanica != null && stanica.ID == gvd.ThisStation.ID)
                            {
                                train.Arrival = train.Departure;
                                train.Departure = null;
                                train.Routing = Routing.Konciaci;
                            }

                            break;
                        }

                        stanica = Station.GetStationFromName(rowsts[0]);
                        if (stanica == null)
                        {
                            wstations.Add($"{rowsts[0]} ({riadok})");
                            continue;
                        }

                        if (stanica.ID == gvd.ThisStation.ID)
                        {
                            thisst = true;

                            if (Utils.TryParseTime(rowsts[count2 - 3], out var prichod)) 
                                train.Arrival = prichod;

                            try
                            {
                                train.Departure = Utils.ParseTime(rowsts[count2 - 2]);
                            }
                            catch (Exception e)
                            {
                                throw new ArgumentException($"String {rowsts[count2 - 2]} sa nedá previesť na DateTime.", e);
                            }

                            continue;
                        }

                        stanica.IsInLongReport = true;

                        //este nebola domovska stanica
                        if (!thisst)
                            train.StaniceZoSmeru.Add(stanica);
                        //uz bola domovska stanica
                        else
                            train.StaniceDoSmeru.Add(stanica);
                    }

                    //nastavenie vychdzej/konecnej stanice
                    var firstZo = train.StaniceZoSmeru.FirstOrDefault();
                    var lastDo = train.StaniceDoSmeru.LastOrDefault();
                    if (firstZo != null)
                    {
                        firstZo.IsInShortReport = true;
                        train.StartingStation = firstZo;
                    }

                    if (lastDo != null)
                    {
                        lastDo.IsInShortReport = true;
                        train.EndingStation = lastDo;
                    }

                    //nastavenie smerovania
                    if (train.StaniceZoSmeru.Count != 0 && train.StaniceDoSmeru.Count != 0)
                        train.Routing = Routing.Prechadzajuci;
                    else if (train.StaniceZoSmeru.Count == 0)
                        train.Routing = Routing.Vychadzajuci;
                    else if (train.StaniceDoSmeru.Count == 0) train.Routing = Routing.Konciaci;

                    //citanie poznamok (dopravca, daterem)
                    if (defOperators)
                    {
                        var lineOp = file.ReadLine();
                        riadok++;
                        if (lineOp != null)
                        {
                            lineOp = lineOp.Substring(2);
                            var sep = lineOp.Split(';');
                            train.Operator = Operator.GetOperatorFromName(operators, sep[0]) ?? Operator.None;
                        }

                        if (!defDaterems)
                        {
                            file.ReadLine();
                        }
                    }

                    if (defDaterems)
                    {
                        var firstl = "";
                        var dateRemIsComming = false;
                        while (true)
                        {
                            var tmp = file.ReadLine();
                            riadok++;
                            if (tmp != null && tmp.StartsWith("* "))
                            {
                                firstl = tmp;
                                dateRemIsComming = true;
                                break;
                            }

                            if (tmp == null || tmp.StartsWith("=")) break;
                        }

                        var lineDateRem = "";
                        if (dateRemIsComming)
                        {
                            firstl = firstl.Substring(2).Trim();
                            lineDateRem += firstl + " ";

                            while (true)
                            {
                                var linedr = file.ReadLine();
                                riadok++;
                                if (linedr != null)
                                {
                                    if (linedr.StartsWith("=")) break;

                                    linedr = linedr.Trim();
                                    lineDateRem += linedr + " ";
                                }
                            }

                            lineDateRem = lineDateRem.Trim();
                            var dr = new DateRem(train.ZaciatokPlatnosti, train.KoniecPlatnosti);
                            dr.TxtToBitArray(lineDateRem); //tu sa vyhodi pripadna vynimka
                            train.DateremText = lineDateRem;
                        }
                    }

                    //nastavenie kolaje
                    train.Track = defaultTrack;

                    //preskocenie
                    file.ReadLine();
                    file.ReadLine();
                    riadok += 2;

                    //ak vlak neobsahuje ThisStation, bude preskoceny
                    //a ak vlak nie je prechadzajuci alebo nema preskocit prechadzajuce tak ma podmienku uskutocnit 
                    var isPassing = train.Routing == Routing.Prechadzajuci;
                    if (thisst && (!isPassing || !omitPassingTrains)) trains.Add(train);
                }
                catch (Exception e)
                {
                    throw new FormatException(string.Format(FORMAT_EX, riadok) + e.Message, e);
                }

            if (wstations.Count != 0)
            {
                var builder = new StringBuilder();
                foreach (var wstation in wstations) builder.Append(wstation + "\n");

                throw new FormatException($"Stanice {builder} nie sú definované.");
            }

            for (var i = 0; i < trains.Count; i++)
            {
                var t1 = trains[i];
                if (t1.Variant != -2) continue;

                var variants = trains.Where((t2, j) => Train.IsSameVariant(t1, t2) && i != j).ToList();

                variants.Add(t1);

                var main = Train.FindMainVariant(variants, out var pos);
                if (main != null)
                {
                    main.Variant = 1;
                    var varID = 2;
                    var rem = new DateRem(main.ZaciatokPlatnosti, main.KoniecPlatnosti, bInsertMarks: false);
                    for (var k = 0; k < variants.Count; k++)
                        if (k != pos)
                        {
                            var var = variants[k];
                            var.DateremText = rem.TxtNot(main.DateremText);
                            var.Variant = varID;
                            varID++;
                        }
                }
                else
                    t1.Variant = -1;
            }

            trains.RemoveRange(0, countdef);
            return trains;
        }

        private static string[] SplitRow(string row)
        {
            var splitted = row.Split(new[] {"  "}, StringSplitOptions.None);
            var output = new List<string>();
            for (var i = 0; i < splitted.Length; i++)
            {
                splitted[i] = splitted[i].Trim();
                if (!string.IsNullOrEmpty(splitted[i])) output.Add(splitted[i]);
            }

            return output.ToArray();
        }
    }
}