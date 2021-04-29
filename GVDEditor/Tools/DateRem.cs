using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.CompilerServices;

namespace GVDEditor.Tools
{
    /// <summary>
    ///     Trieda pre zpracovavanie bitoveho pola do poznamky a naopak
    /// </summary>
    internal class DateRem
    {
        /// <summary>
        ///     Jazyk generovaných datumových poznámok
        /// </summary>
        public enum LOCALE
        {
            CZECH,
            SLOVAK
        }

        /// <summary>
        ///     Kluce k textom
        /// </summary>
        public enum MSG
        {
            ERROR,
            EMPTY,
            RUNSDAILY,
            RUNSNEVER,
            RUNSNEVER_ALT,
            RUNSNEXT,
            RUNSNOT,
            RUNSNOT_ALT,
            RUNS,
            RUNS_ALT,
            FROM,
            TO,
            AND,
            ON,
            MON,
            TUE,
            WED,
            THU,
            FRI,
            SAT,
            SUN,
            WORK,
            HOL,
            JAN,
            FEB,
            MAR,
            APR,
            MAY,
            JUN,
            JUL,
            AUG,
            SEP,
            OCT,
            NOV,
            DEC
        }

        private const int MINCOUNT3 = 80;
        private const int MINCOUNT4 = 14;
        private const int MINCOUNT5 = 30;
        private const int MINWEEKDAYS = 8;

        private const string pconstDayType = "1234567X+";

        private static readonly string[] messagesCZ =
        {
            "chyba",
            "",
            "jede denně",
            "t.č. nejede",
            "nikdy",
            "zatím nejede",
            "nejede ",
            "kromě ",
            "jede ",
            "včetně ",
            "od ",
            "do ",
            " a ",
            "v ",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "X",
            "+",
            "I",
            "II",
            "III",
            "IV",
            "V",
            "VI",
            "VII",
            "VIII",
            "IX",
            "X",
            "XI",
            "XII"
        };

        private static readonly string[] messagesSK =
        {
            "chyba",
            "",
            "ide denne",
            "t.č. nejde",
            "nikdy",
            "zatial nejde",
            "nejde ",
            "okrem ",
            "ide ",
            "vrátane ",
            "od ",
            "do ",
            " a ",
            "v ",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "X",
            "+",
            "I",
            "II",
            "III",
            "IV",
            "V",
            "VI",
            "VII",
            "VIII",
            "IX",
            "X",
            "XI",
            "XII"
        };

        private static readonly string[] messagesRegex =
        {
            "",
            "",
            "^jede denně|^ide denne",
            "^t\\.č\\. ne",
            "",
            "^zat",
            "^n",
            "^kr|^ok",
            "^j|^i",
            "^vč|^vr",
            "^o",
            "^d",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            ""
        };

        private readonly List<ParseData> _parsedData;
        private readonly bool pbAllowRunsDaily;
        private readonly bool pbFromToday;
        private readonly bool pbInsertMarks;
        private readonly bool pbMonthRoman;
        private readonly bool pbSkipDateRangeCheck;
        private readonly bool pbSpecDays;
        private readonly int piMaxDays;
        private readonly StringBuilder _builder;
        private int piDecorLength;
        private int _position;
        private BitArray _bits;
        private string psLastMonth;
        private string _text;

        /// <summary>
        ///     Konstruktor
        /// </summary>
        /// <param name="dtFrom">Pociatocny datum platnosti GVD</param>
        /// <param name="dtTo">Koncovy datum platnosti GVD</param>
        /// <param name="bSpecDays">Pouzivat sviatky a pracovne dni </param>
        /// <param name="bAllowRunsDaily">Vracat tiez - ide denne</param>
        /// <param name="bFromToday">Zohladnit az vzhladom ku dnesku</param>
        /// <param name="bInsertMarks">Vkladat znacky {}</param>
        /// <param name="iMaxDays">Pocet dni do buducnosti</param>
        /// <param name="bMonthRoman">Cisla mesiacov rimskymi cislicami</param>
        /// <param name="bSkipDateRangeCheck">Preskakovat u datumu chyby pre datum mimo grafikonu</param>
        /// <param name="bAltForm">Pouzit zkrateny tvar poznamky</param>
        /// <param name="dtToday">Ktory datum pouzit ako dnesok (ak sa neuvedie, pouzije sa skutocny dnesok)</param>
        public DateRem(DateTime dtFrom, DateTime dtTo, bool bSpecDays = true, bool bAllowRunsDaily = false,
            bool bFromToday = false, bool bInsertMarks = true, int iMaxDays = 0, bool bMonthRoman = true,
            bool bSkipDateRangeCheck = false, bool bAltForm = false, DateTime? dtToday = null)
        {
            pbSpecDays = true;
            pbAllowRunsDaily = true;
            pbMonthRoman = true;
            _builder = new StringBuilder();
            _parsedData = new List<ParseData>();
            DateFrom = dtFrom;
            DateTo = dtTo;
            pbSpecDays = bSpecDays;
            pbAllowRunsDaily = bAllowRunsDaily;
            pbFromToday = bFromToday;
            DateTime dtTodayE = dtToday ?? DateTime.Now;
            Today = dtTodayE == DateTime.MinValue? DateTime.Today : dtTodayE;
            pbInsertMarks = bInsertMarks;
            piMaxDays = iMaxDays;
            pbMonthRoman = bMonthRoman;
            pbSkipDateRangeCheck = bSkipDateRangeCheck;
            AltForm = bAltForm;
            if (DateTo < DateFrom)
                throw new Exception($"Dátum do {dtTo} je menší ako dátum od {dtFrom}.");
            if (TotalDays > 400) throw new Exception($"Príliš dlhý grafikon <{dtFrom},{dtTo}>.");
        }

        /// <summary>
        ///     Jazyk generovanych poznamok
        /// </summary>
        public static LOCALE Loc { get; set; } = LOCALE.SLOVAK;

        /// <summary>
        ///     Celkovy pocet dni grafikonu
        /// </summary>
        public int TotalDays => MaxDay + 1;

        /// <summary>
        ///     Maximalny poradovy index dna
        /// </summary>
        public int MaxDay => DateDiff(DateFrom, DateTo);

        /// <summary>
        ///     Vracia pociatocny datum
        /// </summary>
        public DateTime DateFrom { get; private set; }

        /// <summary>
        ///     Vracia koncovy datum
        /// </summary>
        public DateTime DateTo { get; private set; }

        /// <summary>
        ///     Obdobie platnosti ako text
        /// </summary>
        public string TxtFromTo => $"{FormatDate(DateFrom)} - {FormatDate(DateTo)}";

        /// <summary>
        ///     Príznak alternativneho tvaru textu
        /// </summary>
        public bool AltForm { get; }

        /// <summary>
        ///     Datum pouzity ako "dnes"
        /// </summary>
        public DateTime Today { get; }

        /// <summary>
        ///     Vrati den posunuty vzhladom k zaciatku
        /// </summary>
        public DateTime GetDate(int iDay)
        {
            return DateFrom.AddDays(iDay);
        }

        /// <summary>
        ///     Vytvori bitove pole pre priznaky ide/njede
        /// </summary>
        public BitArray CreateBitArray()
        {
            return new(TotalDays);
        }

        /// <summary>
        ///     Formatuje datum
        /// </summary>
        public static string FormatDate(DateTime dtDate)
        {
            return dtDate.ToString("dd.MM.yyyy");
        }

        /// <summary>
        ///     Vzdialenost medzi datumami ako <see cref="int"/>
        /// </summary>
        public static int DateDiff(DateTime dtFrom, DateTime dtTo)
        {
            return (int) Math.Round((dtTo - dtFrom).TotalDays);
        }

        /// <summary>
        ///     Konvertuje bitove pole do textovej poznamky
        /// </summary>
        /// <param name="oBits">Bitove pole</param>
        /// <param name="iCycle">Posunutie vzhladom k bitovemu polu</param>
        /// <param name="oValidBits"></param>
        /// <returns>
        ///     Text poznamky
        /// </returns>
        public string BitArrayToTxt(BitArray oBits, int iCycle = 0, BitArray oValidBits = null)
        {
            if (oBits == null || oBits.Length != TotalDays)
                throw new ArgumentException(@"Bitové pole na vstupu chýba alebo neodpovedá jeho dĺžka.", nameof(oBits));
            DateTime dtFromSave = DateFrom;
            DateTime dtToSave = DateTo;
            checked
            {
                try
                {
                    DateFrom = DateFrom.AddDays(iCycle);
                    DateTo = DateTo.AddDays(iCycle);
                    var iMaxIndex = MaxDay;
                    if (piMaxDays > 0 && DateTime.Compare(DateTo, Today.AddDays(piMaxDays)) > 0)
                    {
                        DateTo = Today.AddDays(piMaxDays);
                        if (DateTime.Compare(DateTo, DateFrom) < 0) return MsgText(MSG.RUNSNEXT);
                        iMaxIndex = DateDiff(DateFrom, DateTo);
                    }

                    var iMinIndex = 0;
                    if (pbFromToday)
                    {
                        if (DateTime.Compare(Today, DateTo) > 0) return AltMsgText(MSG.RUNSNEVER, MSG.RUNSNEVER_ALT);
                        if (DateTime.Compare(Today, DateFrom) > 0)
                        {
                            iMinIndex = DateDiff(DateFrom, Today);
                            DateFrom = Today;
                        }
                    }

                    var bHasData = false;
                    var bData = oBits[iMinIndex];
                    var num = iMinIndex + 1;
                    var num2 = iMaxIndex;
                    for (var i = num; i <= num2; i++)
                        if (oBits[i] != bData)
                        {
                            bHasData = true;
                            break;
                        }

                    if (!bHasData)
                    {
                        if (!bData) return AltMsgText(MSG.RUNSNEVER, MSG.RUNSNEVER_ALT);
                        if (pbAllowRunsDaily) return MsgText(MSG.RUNSDAILY);
                        return MsgText(MSG.EMPTY);
                    }
                    else
                    {
                        BitArray oValidBits2 = oValidBits;
                        if (iMinIndex > 0 || iMaxIndex != oBits.Length - 1)
                        {
                            int j;
                            if (oValidBits != null && oValidBits.Count == oBits.Count)
                            {
                                oValidBits2 = new BitArray(iMaxIndex - iMinIndex + 1);
                                j = 0;
                                var num3 = iMinIndex;
                                var num4 = iMaxIndex;
                                for (var k = num3; k <= num4; k++)
                                {
                                    oValidBits2[j] = oValidBits[k];
                                    j++;
                                }
                            }

                            var oBits2 = new BitArray(iMaxIndex - iMinIndex + 1);
                            j = 0;
                            var num5 = iMinIndex;
                            var num6 = iMaxIndex;
                            for (var l = num5; l <= num6; l++)
                            {
                                oBits2[j] = oBits[l];
                                j++;
                            }

                            oBits = oBits2;
                        }

                        _bits = oBits;
                        var drLenPos = 0;
                        var iLenPositive = 0;

                        var sPositive = BitArrayToTxt(false, ref drLenPos, ref iLenPositive, oValidBits2);

                        var drLenNeg = 0;
                        var iLenNegative = 0;

                        var sNegative = BitArrayToTxt(true, ref drLenNeg, ref iLenNegative, oValidBits2);

                        if (iLenNegative + (iLenPositive > 40 ? 20 : 25) < iLenPositive || iLenNegative < iLenPositive && drLenNeg < drLenPos) 
                            return sNegative;

                        return sPositive;
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
                finally
                {
                    DateFrom = dtFromSave;
                    DateTo = dtToSave;
                    _bits = null;
                }

                return "";
            }
        }

        /// <summary>
        ///     Konvertuje textovu poznamku do bitoveho pole
        /// </summary>
        /// <param name="sTxt">Text poznamky</param>
        /// <returns>
        ///     Text poznamky
        /// </returns>
        public BitArray TxtToBitArray(string sTxt)
        {
            try
            {
                if (string.IsNullOrEmpty(sTxt) || TokenIsMsg(sTxt.Trim(), MSG.RUNSDAILY, MSG.RUNS, MSG.RUNS_ALT))
                    return new BitArray(TotalDays, true);
                if (TokenIsMsg(sTxt.Trim(), MSG.RUNSNEVER, MSG.RUNSNEVER_ALT, MSG.RUNSNOT, MSG.RUNSNOT_ALT))
                    return CreateBitArray();
                _bits = CreateBitArray();
                _text = sTxt;
                _position = 0;
                _parsedData.Clear();
                TxtParse();
                return _bits;
            }
            catch (ParseException)
            {
                throw;
            }
            catch (Exception)
            {
                // ignored
            }

            return null;
        }

        public bool ValidTxt(string sTxt)
        {
            try
            {
                TxtToBitArray(sTxt);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool Overlap(string d1, string d2)
        {
            var obmand = TxtAnd(d1, d2);
            return obmand != string.Empty && obmand != "t.č. nejde" && obmand != "t.č. nejede";
        }

        /// <summary>
        ///     Normalizuje text
        /// </summary>
        public string TxtNormalize(string sTxt)
        {
            return TxtShift(sTxt, 0);
        }

        /// <summary>
        ///     Posunie text o zadany cykus
        /// </summary>
        public string TxtShift(string sTxt, int iCycle)
        {
            return BitArrayToTxt(TxtToBitArray(sTxt), iCycle);
        }

        /// <summary>
        ///     Logicka operacia AND medzi textami
        /// </summary>
        public string TxtAnd(params string[] texts)
        {
            BitArray arr;
            if (texts.Length > 1)
                arr = TxtToBitArray(texts[0]);
            else if (texts.Length == 1)
                return texts[0];
            else
                return string.Empty;

            for (var i = 1; i < texts.Length; i++) arr.And(TxtToBitArray(texts[i]));

            return BitArrayToTxt(arr);
        }

        /// <summary>
        ///     Logicka operacia NOT podla textu
        /// </summary>
        public string TxtNot(string sTxt1)
        {
            return BitArrayToTxt(TxtToBitArray(sTxt1).Not());
        }

        /// <summary>
        ///     Logicka operacia OR medzi textami
        /// </summary>
        public string TxtOr(params string[] texty)
        {
            var obm = "";
            foreach (var text in texty) obm = BitArrayToTxt(TxtToBitArray(obm).Or(TxtToBitArray(text)));

            return obm;
        }

        /// <summary>
        ///     Text správy
        /// </summary>
        public static string MsgText(MSG iMsg)
        {
            var MsgText = Loc switch
            {
                LOCALE.CZECH => messagesCZ[(int) iMsg],
                LOCALE.SLOVAK => messagesSK[(int) iMsg],
                _ => "?"
            };

            return MsgText;
        }

        /// <summary>
        ///     Text správy so zohlednenim alternativnej formulacie
        /// </summary>
        public string AltMsgText(MSG iMsg1, MSG iMsg2)
        {
            return MsgText(AltForm ? iMsg2 : iMsg1);
        }

        private static void ReduceDates(IList<DateRemInfo> aoDatumoveObmedzenie, BitArray oValidBits)
        {
            checked
            {
                for (var i = aoDatumoveObmedzenie.Count - 1; i >= 0; i += -1)
                {
                    DateRemInfo dateRemInfo = aoDatumoveObmedzenie[i];
                    if (dateRemInfo.aoRuns != null && dateRemInfo.aoRuns.Count > 0)
                        ReduceDates(dateRemInfo.aoRuns, oValidBits);
                    if (dateRemInfo.aoRunsNot != null && dateRemInfo.aoRunsNot.Count > 0)
                        ReduceDates(dateRemInfo.aoRunsNot, oValidBits);
                    if (dateRemInfo.type == DAYTYPE.NONE && dateRemInfo.from > 0 &&
                        dateRemInfo.from == dateRemInfo.to &&
                        !oValidBits[dateRemInfo.from]) aoDatumoveObmedzenie.RemoveAt(i);
                }
            }
        }

        private string BitArrayToTxt(bool bIsNot, ref int iDatumoveObmedzenieLen, ref int iLen, BitArray oValidBits)
        {
            if (bIsNot) _bits.Not();
            string BitArrayToTxt = null;
            try
            {
                var aoDatumoveObmedzenie = ProcessInterval(MINCOUNT3, 0, MaxDay);
                if (oValidBits != null && oValidBits.Length == _bits.Length && aoDatumoveObmedzenie != null &&
                    aoDatumoveObmedzenie.Count > 0) ReduceDates(aoDatumoveObmedzenie, oValidBits);

                if (aoDatumoveObmedzenie != null)
                {
                    iDatumoveObmedzenieLen = aoDatumoveObmedzenie.Count;
                    piDecorLength = 0;
                    var s = FormatDatumoveObmedzenie(aoDatumoveObmedzenie, bIsNot);
                    if (string.IsNullOrEmpty(s))
                    {
                        iLen = 0;
                    }
                    else
                    {
                        if (AltForm)
                        {
                            if (s.StartsWith(MsgText(MSG.RUNS_ALT))) s = s.Substring(MsgText(MSG.RUNS_ALT).Length);

                            s = s.Replace(MsgText(MSG.RUNS_ALT) + MsgText(MSG.ON), MsgText(MSG.RUNS_ALT));
                            s = s.Replace(MsgText(MSG.RUNSNOT_ALT) + MsgText(MSG.ON), MsgText(MSG.RUNSNOT_ALT));
                        }

                        iLen = checked(s.Length - piDecorLength);
                    }

                    BitArrayToTxt = s;
                }
            }
            finally
            {
                if (bIsNot) _bits.Not();
            }

            return BitArrayToTxt;
        }

        private List<DateRemInfo> ProcessInterval(int iMinCount3, int iFrom, int iTo)
        {
            ReduceInterval(ref iFrom, ref iTo);
            var daterem = GetSingleDays(iFrom, iTo);
            checked
            {
                List<DateRemInfo> ProcessInterval;
                if (daterem != null)
                {
                    ProcessInterval = daterem;
                }
                else
                {
                    var aiOKCount = new int[9];
                    var aiBadCount = new int[9];
                    var iIsFC = 0;
                    var bScanWeekDaysOK = false;
                    if (ScanDays(iFrom, iTo, aiOKCount, aiBadCount, ref iIsFC) && !AllSet(aiOKCount) && !NoBad(aiBadCount) && iTo - iFrom > 6)
                    {
                        bScanWeekDaysOK = true;
                    }
                    else
                    {
                        daterem = GetIntervals(iFrom, iTo);
                        if (daterem != null) return daterem;
                        goto IL_74;
                    }

                    IL_B8:
                    for (;;)
                    {
                        daterem = ScanWeekDays(iMinCount3, iFrom, iTo);
                        if (daterem != null) break;
                        var i = iMinCount3 >> 1;
                        if (i <= 0) i = 1;
                        iMinCount3 -= i;
                        if (!bScanWeekDaysOK) goto IL_74;
                    }

                    return daterem;
                    IL_74:
                    daterem = Recurse1(iMinCount3, iFrom, iTo);
                    if (daterem != null)
                    {
                        ProcessInterval = daterem;
                    }
                    else
                    {
                        daterem = Recurse2(iMinCount3, iFrom, iTo);
                        if (daterem != null)
                        {
                            ProcessInterval = daterem;
                        }
                        else
                        {
                            daterem = Recurse3(iMinCount3, iFrom, iTo);
                            if (daterem != null)
                            {
                                ProcessInterval = daterem;
                            }
                            else
                            {
                                daterem = Recurse4(iMinCount3, iFrom, iTo);
                                if (daterem == null) goto IL_B8;
                                ProcessInterval = daterem;
                            }
                        }
                    }
                }

                return ProcessInterval;
            }
        }

        private List<DateRemInfo> Recurse1(int iMinCount3, int iFrom, int iTo)
        {
            List<DateRemInfo> daterem = null;
            var i = -1;
            checked
            {
                for (var j = iFrom; j <= iTo; j++)
                    if (Runs(j))
                    {
                        if (i < 0) i = j;
                    }
                    else
                    {
                        var l = 0;
                        l++;
                        if (l >= iMinCount3 && i >= 0)
                        {
                            var k = 0;
                            AppendDatumoveObmedzenie(ref daterem, ProcessInterval(iMinCount3, i, k));
                            i = -1;
                        }
                    }

                if (i > iFrom && daterem != null)
                    AppendDatumoveObmedzenie(ref daterem, ProcessInterval(iMinCount3, i, iTo));
                return daterem;
            }
        }

        private List<DateRemInfo> Recurse2(int iMinCount3, int iFrom, int iTo)
        {
            List<DateRemInfo> daterem = null;
            var i = -1;
            var k = 0;
            checked
            {
                for (var j = iFrom; j <= iTo; j++)
                    if (Runs(j))
                    {
                        k++;
                        if (i < 0) i = j;
                    }
                    else
                    {
                        if (k >= MINCOUNT5)
                        {
                            if (i > iFrom) daterem = ProcessInterval(iMinCount3, iFrom, i - 1);
                            AppendDatumoveObmedzenie(ref daterem, new DateRemInfo(i, j - 1));
                            AppendDatumoveObmedzenie(ref daterem, ProcessInterval(iMinCount3, j, iTo));
                            return daterem;
                        }

                        k = 0;
                        i = -1;
                    }

                return null;
            }
        }

        private List<DateRemInfo> Recurse3(int iMinCount3, int iFrom, int iTo)
        {
            List<DateRemInfo> aoDatumoveObmedzenie = null;
            var i = iFrom;
            checked
            {
                var j = 0;
                while (i <= iTo && Runs(i))
                {
                    j++;
                    i++;
                }

                List<DateRemInfo> Recurse3;
                if (j >= MINCOUNT4)
                {
                    AppendDatumoveObmedzenie(ref aoDatumoveObmedzenie, new DateRemInfo(iFrom, iFrom + j - 1));
                    AppendDatumoveObmedzenie(ref aoDatumoveObmedzenie, ProcessInterval(iMinCount3, iFrom + j, iTo));
                    Recurse3 = aoDatumoveObmedzenie;
                }
                else
                {
                    Recurse3 = null;
                }

                return Recurse3;
            }
        }

        private List<DateRemInfo> Recurse4(int iMinCount3, int iFrom, int iTo)
        {
            var i = iTo;
            checked
            {
                var j = 0;
                while (i >= iFrom && Runs(i))
                {
                    j++;
                    i += -1;
                }

                List<DateRemInfo> Recurse4;
                if (j >= MINCOUNT4)
                {
                    var aoDatumoveObmedzenie = ProcessInterval(iMinCount3, iFrom, iTo - j);
                    AppendDatumoveObmedzenie(ref aoDatumoveObmedzenie, new DateRemInfo(iTo - j + 1, iTo));
                    Recurse4 = aoDatumoveObmedzenie;
                }
                else
                {
                    Recurse4 = null;
                }

                return Recurse4;
            }
        }

        private List<DateRemInfo> ScanWeekDays(int iMinCount3, int iFrom, int iTo)
        {
            List<DateRemInfo> aoDatumoveObmedzenie = null;
            var aiOKCount = new int[9];
            var aiBadCount = new int[9];
            checked
            {
                var num = iFrom + 7;
                for (var iTo2 = iTo; iTo2 >= num; iTo2 += -1)
                {
                    if (iTo2 >= iTo || !RunsNot(iTo2))
                    {
                        var iIsFC = 0;
                        ScanDays(iFrom, iTo2, aiOKCount, aiBadCount, ref iIsFC);
                        var i = 0;
                        var bIsOK = false;
                        do
                        {
                            if (aiOKCount[i] > aiBadCount[i] * 2 && aiBadCount[i] <= 8)
                            {
                                aiOKCount[i] = 1;
                                bIsOK = true;
                            }
                            else
                            {
                                aiOKCount[i] = 0;
                            }

                            i++;
                        } while (i <= 8);

                        if (bIsOK)
                        {
                            var j = 0;
                            var num2 = iFrom;
                            var num3 = iTo2;

                            for (i = num2; i <= num3; i++)
                                if (Runs(i) && aiOKCount[(int) GetDayIndex(i, iIsFC)] == 0 &&
                                    (i == iFrom || RunsNot(i - 1) || aiOKCount[(int) GetDayIndex(i - 1, iIsFC)] != 0))
                                    j++;

                            var k = 0;
                            i = iFrom;

                            while (i <= iTo2)
                                if (aiOKCount[(int) GetDayIndex(i, iIsFC)] != 0 && RunsNot(i))
                                {
                                    k++;
                                    while (i <= iTo2)
                                    {
                                        if (!RunsNot(i)) break;
                                        i++;
                                    }
                                }
                                else
                                    i++;

                            if (j + k <= 13 || iTo2 - iFrom > 350 && j + k <= 19)
                            {
                                if (k > 0)
                                {
                                    i = iTo2;
                                    while (aiOKCount[(int) GetDayIndex(i, iIsFC)] == 0) i--;
                                    if (RunsNot(i)) continue;
                                }

                                if (iTo2 < iTo || GetBetterDayTo(iTo2, aiOKCount, iIsFC) < MaxDay)
                                {
                                    i = 0;
                                    while (!((aiOKCount[(int) GetDayIndex(iTo2 - i, iIsFC)] != 0) ^ Runs(iTo2 - i)))
                                    {
                                        i++;
                                        if (i > 6) goto IL_18F;
                                    }

                                    goto IL_4CB;
                                }

                                IL_18F:
                                if ((j != 0 || k != 0) && iTo2 < iTo - 21 && iTo2 > 13 &&
                                    (EqualPatt(iTo2 - 6, iTo2 + 1) && EqualPatt(iTo2 - 6, iTo2 + 8) &&
                                     EqualPatt(iTo2 - 6, iTo2 + 15) ||
                                     EqualPatt(iTo2 - 13, iTo2 + 1) && EqualPatt(iTo2 - 13, iTo2 + 8) &&
                                     EqualPatt(iTo2 - 13, iTo2 + 15))) continue;
                                if (j + k > 3 && iTo2 - iFrom > 35)
                                {
                                    var aiOKCount2 = new int[9];
                                    var aiBadCount2 = new int[9];
                                    var iIsFC2 = 0;

                                    if (ScanDays(iFrom, iFrom + 20, aiOKCount2, aiBadCount2, ref iIsFC2) &&
                                        GetDayType(aiOKCount) != GetDayType(aiOKCount2))
                                    {
                                        var num4 = iFrom + 20;
                                        var num5 = iTo2 - 1;
                                        i = num4;
                                        while (i <= num5 && ScanDays(iFrom, i, aiOKCount2, aiBadCount2, ref iIsFC2))
                                            i++;
                                        iTo2 = i - 1;
                                        AppendDatumoveObmedzenie(ref aoDatumoveObmedzenie, ProcessInterval(iMinCount3, iFrom, iTo2));
                                        goto IL_4CB;
                                    }
                                }

                                var _DayFrom = GetBetterDayFrom(iFrom, aiOKCount, iIsFC);
                                var _DayTo = GetBetterDayTo(iTo2, aiOKCount, iIsFC);

                                if (_DayFrom > 0 && aiOKCount[(int) GetDayIndex(_DayFrom, iIsFC)] == 0)
                                {
                                    i = _DayFrom;
                                    while (Runs(_DayFrom)) _DayFrom++;
                                    if (_DayFrom > i)
                                    {
                                        AppendDatumoveObmedzenie(ref aoDatumoveObmedzenie, ProcessInterval(iMinCount3, i, _DayFrom - 1));
                                        while (RunsNot(_DayFrom)) _DayFrom++;
                                        iFrom = _DayFrom;
                                    }
                                }

                                var o = new DateRemInfo(_DayFrom, _DayTo);
                                if (_DayFrom != _DayTo)
                                    o.type = CheckDayType(_DayFrom, _DayTo, GetDayType(aiOKCount));

                                AppendDatumoveObmedzenie(ref aoDatumoveObmedzenie, o);

                                if (j > 0)
                                {
                                    i = iFrom;
                                    while (i <= iTo2)
                                        if (aiOKCount[(int) GetDayIndex(i, iIsFC)] == 0 && Runs(i))
                                        {
                                            j = i;
                                            while (i <= iTo2 && Runs(i)) i++;
                                            var l = i - 1;
                                            while (aiOKCount[(int) GetDayIndex(l, iIsFC)] != 0) l--;
                                            AppendPeriodDatumoveObmedzenie(
                                                ref aoDatumoveObmedzenie[aoDatumoveObmedzenie.Count - 1].aoRuns, j, l);
                                        }
                                        else
                                            i++;
                                }

                                if (k > 0)
                                {
                                    i = iFrom;

                                    while (i <= iTo2)
                                        if (aiOKCount[(int) GetDayIndex(i, iIsFC)] != 0 && RunsNot(i))
                                        {
                                            j = i;
                                            k = i;
                                            var iPocetOKDnu = 0;
                                            var aiOKDen = new int[4];
                                            while (i <= iTo2 && RunsNot(i))
                                            {
                                                if (aiOKCount[(int) GetDayIndex(i, iIsFC)] != 0)
                                                {
                                                    k = i;
                                                    if (iPocetOKDnu < 2) aiOKDen[iPocetOKDnu] = k;
                                                    iPocetOKDnu++;
                                                }

                                                i++;
                                            }

                                            if (iPocetOKDnu < 3)
                                            {
                                                var num6 = iPocetOKDnu - 1;
                                                for (k = 0; k <= num6; k++)
                                                    AppendDayDatumoveObmedzenie(
                                                        ref aoDatumoveObmedzenie[aoDatumoveObmedzenie.Count - 1]
                                                            .aoRunsNot, aiOKDen[k]);
                                            }
                                            else
                                            {
                                                while (j >= _DayFrom && RunsNot(j)) j--;
                                                j++;
                                                while (k <= _DayTo && RunsNot(k)) k++;
                                                k--;
                                                AppendPeriodDatumoveObmedzenie(
                                                    ref aoDatumoveObmedzenie[aoDatumoveObmedzenie.Count - 1].aoRunsNot,
                                                    j, k);
                                                if (k > i) i = k;
                                            }
                                        }
                                        else
                                            i++;
                                }
                            }
                        }

                        IL_4CB:
                        if (aoDatumoveObmedzenie != null)
                        {
                            if (iTo2 < iTo)
                                AppendDatumoveObmedzenie(ref aoDatumoveObmedzenie,
                                    ProcessInterval(iMinCount3, iTo2 + 1, iTo));
                            break;
                        }
                    }
                }

                return aoDatumoveObmedzenie;
            }
        }

        private void ReduceInterval(ref int iFrom, ref int iTo)
        {
            checked
            {
                while (iFrom < iTo)
                {
                    if (!RunsNot(iFrom)) break;
                    iFrom++;
                }

                while (iFrom < iTo && RunsNot(iTo)) iTo--;
            }
        }

        private List<DateRemInfo> GetIntervals(int iFrom, int iTo)
        {
            checked
            {
                var num = iTo - 1;
                var iCount = 0;
                var iCount2 = 0;
                for (var i = iFrom; i <= num; i++)
                    if (Runs(i) && RunsNot(i + 1))
                        iCount++;
                    else if (RunsNot(i) && Runs(i + 1)) iCount2++;
                if (Runs(iTo))
                    iCount++;
                else
                    iCount2++;
                List<DateRemInfo> GetIntervals;
                if (iCount <= 2 && iCount <= iCount2 || iCount == 1 && iCount2 == 0)
                {
                    var oDatumoveObmedzenie = new DateRemInfo();
                    var j = -1;
                    for (var k = iFrom; k <= iTo; k++)
                        if (Runs(k))
                        {
                            if (j < 0) j = k;
                        }
                        else if (j >= 0)
                        {
                            AppendPeriodDatumoveObmedzenie(ref oDatumoveObmedzenie.aoRuns, j, k - 1);
                            j = -1;
                        }

                    if (j >= 0) AppendPeriodDatumoveObmedzenie(ref oDatumoveObmedzenie.aoRuns, j, iTo);
                    GetIntervals = new List<DateRemInfo>
                    {
                        oDatumoveObmedzenie
                    };
                }
                else
                {
                    GetIntervals = null;
                }

                return GetIntervals;
            }
        }

        private List<DateRemInfo> GetSingleDays(int iFrom, int iTo)
        {
            checked
            {
                var j = 0;
                for (var i = iFrom; i <= iTo; i++)
                    if (Runs(i))
                        j++;
                List<DateRemInfo> GetSingleDays;
                if (j == 0)
                {
                    GetSingleDays = null;
                }
                else
                {
                    var aiOKCount = new int[9];
                    var aiBadCount = new int[9];
                    var iIsFC = 0;
                    var bDaysOK = ScanDays(iFrom, iTo, aiOKCount, aiBadCount, ref iIsFC);
                    if (bDaysOK || j <= 6 && (j == iTo - iFrom + 1 || j <= iTo - iFrom + 2 - j))
                    {
                        var oDatumoveObmedzenie = new DateRemInfo();
                        if (bDaysOK && iTo - iFrom > 8)
                        {
                            oDatumoveObmedzenie.from = GetBetterDayFrom(iFrom, aiOKCount, iIsFC);
                            oDatumoveObmedzenie.to = GetBetterDayTo(iTo, aiOKCount, iIsFC);
                            oDatumoveObmedzenie.type = GetDayType(aiOKCount);
                        }
                        else
                        {
                            for (var i = iFrom; i <= iTo; i++)
                                if (Runs(i))
                                    AppendDayDatumoveObmedzenie(ref oDatumoveObmedzenie.aoRuns, i);
                        }

                        GetSingleDays = new List<DateRemInfo>
                        {
                            oDatumoveObmedzenie
                        };
                    }
                    else
                    {
                        GetSingleDays = null;
                    }
                }

                return GetSingleDays;
            }
        }

        private void AppendDatumoveObmedzenie(ref List<DateRemInfo> aoList, List<DateRemInfo> aoList2)
        {
            if (aoList == null)
            {
                aoList = aoList2;
                return;
            }

            aoList.AddRange(aoList2);
        }

        private static void AppendDatumoveObmedzenie(ref List<DateRemInfo> aoList, DateRemInfo oInfo)
        {
            aoList ??= new List<DateRemInfo>();
            aoList.Add(oInfo);
        }

        private void AppendDayDatumoveObmedzenie(ref List<DateRemInfo> aoRuns, int iDay)
        {
            AppendPeriodDatumoveObmedzenie(ref aoRuns, iDay, iDay);
        }

        private static void AppendPeriodDatumoveObmedzenie(ref List<DateRemInfo> aoRuns, int iFrom, int iTo)
        {
            checked
            {
                if (iFrom > 0 && aoRuns.Count > 0 && aoRuns[aoRuns.Count - 1].to == iFrom - 1)
                {
                    aoRuns[aoRuns.Count - 1].to = iTo;
                    return;
                }

                aoRuns.Add(new DateRemInfo(iFrom, iTo));
            }
        }

        private bool ScanDays(int iFrom, int iTo, int[] aiOKCount, int[] aiBadCount, ref int iIsFC)
        {
            Array.Clear(aiOKCount, 0, aiOKCount.Length);
            Array.Clear(aiBadCount, 0, aiBadCount.Length);
            DAYINDEX iDayIndex = GetDayIndex(iFrom, 0);
            checked
            {
                var iSaturdays = 0;
                var iTotalBad = 0;
                for (var i = iFrom; i <= iTo; i++)
                {
                    if (Runs(i))
                    {
                        ref var ptr = ref aiOKCount[7];
                        if (pbSpecDays)
                        {
                            DAYTYPE iType = GetDayType(i);
                            if ((iType & DAYTYPE.WORKDAY) != DAYTYPE.NONE)
                            {
                                var num = 7;
                                ptr = ref aiOKCount[num];
                                aiOKCount[num] = ptr + 1;
                            }
                            else if ((iType & DAYTYPE.HOLIDAY) != DAYTYPE.NONE)
                            {
                                var num2 = 8;
                                ptr = ref aiOKCount[num2];
                                aiOKCount[num2] = ptr + 1;
                                if (iDayIndex == DAYINDEX.SATURDAY) iSaturdays++;
                            }
                        }

                        DAYINDEX dayindex = iDayIndex;
                        ptr = ref aiOKCount[(int) dayindex];
                        aiOKCount[(int) dayindex] = ptr + 1;
                    }
                    else
                    {
                        ref var ptr = ref aiBadCount[7];
                        if (pbSpecDays)
                        {
                            DAYTYPE iType2 = GetDayType(i);
                            if ((iType2 & DAYTYPE.WORKDAY) != DAYTYPE.NONE)
                            {
                                var num3 = 7;
                                ptr = ref aiBadCount[num3];
                                aiBadCount[num3] = ptr + 1;
                            }
                            else if ((iType2 & DAYTYPE.HOLIDAY) != DAYTYPE.NONE)
                            {
                                var num4 = 8;
                                ptr = ref aiBadCount[num4];
                                aiBadCount[num4] = ptr + 1;
                            }
                        }

                        DAYINDEX dayindex2 = iDayIndex;
                        ptr = ref aiBadCount[(int) dayindex2];
                        aiBadCount[(int) dayindex2] = ptr + 1;
                        iTotalBad++;
                    }

                    iDayIndex = GetNextDayIndex(iDayIndex);
                }

                if (iTotalBad != 0)
                {
                    if (pbSpecDays)
                    {
                        if (aiOKCount[5] > 2 * aiBadCount[5] && aiOKCount[8] < 2 * aiBadCount[8])
                        {
                            var num5 = 8;
                            ref var ptr = ref aiOKCount[num5];
                            aiOKCount[num5] = ptr - iSaturdays;
                        }

                        var j = DAYINDEX.MONDAY;
                        var iCount = 0;
                        var iCount2 = 0;
                        do
                        {
                            if (aiOKCount[(int) j] > 0 && aiOKCount[(int) j] > aiBadCount[(int) j])
                            {
                                if (j <= DAYINDEX.SUNDAY) iCount += aiOKCount[(int) j] - aiBadCount[(int) j];
                                if (j == DAYINDEX.SATURDAY || j == DAYINDEX.WORKDAY || j == DAYINDEX.HOLIDAY)
                                    iCount2 += aiOKCount[(int) j] - aiBadCount[(int) j];
                            }

                            unchecked
                            {
                                j++;
                            }
                        } while (j <= DAYINDEX.HOLIDAY);

                        if (iCount2 > iCount)
                        {
                            iIsFC = 1;
                            var k = DAYINDEX.MONDAY;
                            unchecked
                            {
                                do
                                {
                                    if (k != DAYINDEX.SATURDAY)
                                    {
                                        aiOKCount[(int) k] = 0;
                                        aiBadCount[(int) k] = 0;
                                    }

                                    k++;
                                } while (k <= DAYINDEX.SUNDAY);
                            }

                            if (aiOKCount[8] > 2 * aiBadCount[8] && aiOKCount[5] < 2 * aiBadCount[5])
                            {
                                var num6 = 5;
                                ref var ptr = ref aiOKCount[num6];
                                aiOKCount[num6] = ptr - iSaturdays;
                            }
                            else
                            {
                                iIsFC |= 32;
                            }
                        }
                        else
                        {
                            aiOKCount[7] = 0;
                            aiBadCount[7] = 0;
                            aiOKCount[8] = 0;
                            aiBadCount[8] = 0;
                        }
                    }

                    var l = DAYINDEX.MONDAY;
                    unchecked
                    {
                        while (aiOKCount[(int) l] == 0 || aiBadCount[(int) l] == 0)
                        {
                            l++;
                            if (l > DAYINDEX.HOLIDAY) return true;
                        }
                    }
                }

                return false;
            }
        }

        private int GetBetterDayFrom(int iFrom, int[] aiOKCount, int iIsFC)
        {
            var iFrom2 = iFrom;
            checked
            {
                if (iFrom < 7)
                {
                    while (iFrom >= 0 && (Runs(iFrom) || aiOKCount[(int) GetDayIndex(iFrom, iIsFC)] == 0)) iFrom--;
                    if (iFrom < 0)
                    {
                        iFrom = 0;
                    }
                    else
                    {
                        iFrom = iFrom2;
                        while (aiOKCount[(int) GetDayIndex(iFrom, iIsFC)] == 0) iFrom++;
                    }
                }

                return iFrom;
            }
        }

        private int GetBetterDayTo(int iTo, int[] aiOKCount, int iIsFC)
        {
            var iTo2 = iTo;
            var iDays = MaxDay;
            checked
            {
                if (iTo > iDays - 7)
                {
                    while (iTo <= iDays && (Runs(iTo) || aiOKCount[(int) GetDayIndex(iTo, iIsFC)] == 0)) iTo++;
                    if (iTo > iDays)
                    {
                        iTo = iDays;
                    }
                    else
                    {
                        iTo = iTo2;
                        while (aiOKCount[(int) GetDayIndex(iTo, iIsFC)] == 0) iTo--;
                    }
                }

                return iTo;
            }
        }

        private static bool AllSet(int[] aiCount)
        {
            DAYTYPE iDayType = GetDayType(aiCount);
            return (iDayType & DAYTYPE.ALL1) == DAYTYPE.ALL1 || (iDayType & DAYTYPE.ALL2) == DAYTYPE.ALL2;
        }

        private static bool NoBad(int[] aiCount)
        {
            foreach (var t in aiCount)
                if (t != 0)
                    return false;

            return true;
        }

        private bool Runs(int iDay)
        {
            return _bits[iDay];
        }

        private bool RunsNot(int iDay)
        {
            return !Runs(iDay);
        }

        private bool EqualPatt(int iFrom, int iTo)
        {
            var i = 0;
            checked
            {
                while (Runs(iFrom + i) == Runs(iTo + i))
                {
                    i++;
                    if (i > 6) return true;
                }

                return false;
            }
        }

        private string FormatDatumoveObmedzenie(List<DateRemInfo> daterem, bool bIsNot)
        {
            checked
            {
                string formatDateRem;
                if (daterem == null || daterem.Count == 0)
                {
                    formatDateRem = MsgText(MSG.EMPTY);
                }
                else
                {
                    MergeDatumoveObmedzenie(daterem);
                    _builder.Length = 0;
                    var iLevel = LEVEL.UNDEF;
                    var num = daterem.Count - 1;
                    for (var i = 0; i <= num; i++)
                        if (i + 1 < daterem.Count)
                            FormatDatumoveObmedzenie1(daterem[i], daterem[i + 1], bIsNot,
                                ref iLevel);
                        else
                            FormatDatumoveObmedzenie1(daterem[i], null, bIsNot, ref iLevel);
                    formatDateRem = _builder.ToString();
                }

                return formatDateRem;
            }
        }

        private void FormatDatumoveObmedzenie1(DateRemInfo daterem, DateRemInfo dateremInfo, bool bIsNot, ref LEVEL iLevel)
        {
            AppendComma();
            psLastMonth = null;
            if (daterem.AllSet && daterem.from == 0 && daterem.to == MaxDay && !daterem.RunsNot)
            {
                _builder.Append(MsgText(MSG.RUNSDAILY));
                return;
            }

            var iBaseLength = _builder.Length;
            checked
            {
                if (daterem.HaveDays || daterem.from != 0 || daterem.to != 0)
                {
                    AppendPeriod(daterem.from, daterem.to);
                    if (daterem.HaveDays && dateremInfo != null && dateremInfo.type == daterem.type && !daterem.Runs &&
                        !daterem.RunsNot)
                        _builder.Append(MsgText(MSG.AND));
                    else if (daterem.HaveDays) AppendDays(daterem.type);
                    if (_builder.Length > iBaseLength)
                    {
                        if (bIsNot)
                        {
                            if (iLevel != LEVEL.RUNSNOT)
                            {
                                _builder.Insert(iBaseLength, AltMsgText(MSG.RUNSNOT, MSG.RUNSNOT_ALT));
                                iLevel = LEVEL.RUNSNOT;
                            }
                            else if (dateremInfo == null && iBaseLength > 0 && _builder[iBaseLength - 1] == ',' && !daterem.HaveDays)
                            {
                                iBaseLength--;
                                _builder.Remove(iBaseLength, 1);
                                _builder.Insert(iBaseLength, MsgText(MSG.AND));
                            }
                        }
                        else if (iLevel != LEVEL.RUNS)
                        {
                            _builder.Insert(iBaseLength, AltMsgText(MSG.RUNS, MSG.RUNS_ALT));
                            iLevel = LEVEL.RUNS;
                        }
                        else if (dateremInfo == null && iBaseLength > 0 && _builder[iBaseLength - 1] == ',' && !daterem.HaveDays)
                        {
                            iBaseLength--;
                            _builder.Remove(iBaseLength, 1);
                            _builder.Insert(iBaseLength, MsgText(MSG.AND));
                        }
                    }
                }

                var num = daterem.aoRuns.Count - 1;
                for (var i = 0; i <= num; i++)
                {
                    if (_builder.Length == iBaseLength)
                    {
                        if (bIsNot)
                        {
                            if (iLevel != LEVEL.RUNSNOT)
                            {
                                _builder.Append(AltMsgText(MSG.RUNSNOT, MSG.RUNSNOT_ALT));
                                iLevel = LEVEL.RUNSNOT;
                            }
                        }
                        else if (iLevel != LEVEL.RUNS)
                        {
                            _builder.Append(AltMsgText(MSG.RUNS, MSG.RUNS_ALT));
                            iLevel = LEVEL.RUNS;
                        }
                    }

                    AppendPeriod(daterem.aoRuns[i].from, daterem.aoRuns[i].to);
                }

                psLastMonth = null;
                var num2 = daterem.aoRunsNot.Count - 1;
                for (var j = 0; j <= num2; j++)
                {
                    AppendComma();
                    if (j == 0)
                    {
                        if (bIsNot)
                        {
                            if (iLevel != LEVEL.RUNS)
                            {
                                _builder.Append(AltMsgText(MSG.RUNS, MSG.RUNS_ALT));
                                iLevel = LEVEL.RUNS;
                            }
                        }
                        else if (iLevel != LEVEL.RUNSNOT)
                        {
                            _builder.Append(AltMsgText(MSG.RUNSNOT, MSG.RUNSNOT_ALT));
                            iLevel = LEVEL.RUNSNOT;
                        }
                    }

                    AppendPeriod(daterem.aoRunsNot[j].from, daterem.aoRunsNot[j].to);
                }
            }
        }

        private static void MergeDatumoveObmedzenie(IList<DateRemInfo> aoDatumoveObmedzenie)
        {
            checked
            {
                var i = 0;
                while (i + 1 < aoDatumoveObmedzenie.Count)
                    if (aoDatumoveObmedzenie[i].Merge(aoDatumoveObmedzenie[i + 1]))
                        aoDatumoveObmedzenie.RemoveAt(i + 1);
                    else
                        i++;
            }
        }

        private void AppendDays(DAYTYPE iDayType)
        {
            AppendSpace();
            _builder.Append(MsgText(MSG.ON));
            if ((iDayType & DAYTYPE.WORKDAY) == DAYTYPE.WORKDAY) _builder.Append(MsgDayType(MSG.WORK));
            var i = 0;
            checked
            {
                while (true)
                    if (i > 6 || (iDayType & (DAYTYPE) (1 << i)) != DAYTYPE.NONE)
                    {
                        if (i > 6) break;
                        var j = i;
                        while (j < 6 && (iDayType & (DAYTYPE) (1 << (j + 1))) != DAYTYPE.NONE) j++;
                        if (j - i > 1)
                        {
                            AppendComma();
                            _builder.Append(MsgDayType(MSG.MON + i)).Append("-").Append(MsgDayType(MSG.MON + j));
                        }
                        else
                        {
                            while (i <= j)
                            {
                                AppendComma();
                                _builder.Append(MsgDayType(MSG.MON + i));
                                i++;
                            }
                        }

                        i = j + 1;
                        if (i > 6) break;
                    }
                    else
                    {
                        i++;
                    }

                if ((iDayType & DAYTYPE.HOLIDAY) == DAYTYPE.HOLIDAY)
                {
                    AppendComma();
                    _builder.Append(MsgDayType(MSG.HOL));
                }
            }
        }

        private void AppendPeriod(int iDayFrom, int iDayTo)
        {
            checked
            {
                if (iDayFrom != 0 || iDayTo != MaxDay)
                {
                    if (iDayTo - iDayFrom <= 1)
                    {
                        for (var i = iDayFrom; i <= iDayTo; i++) AppendDay(i);
                        return;
                    }

                    AppendComma();
                    if (iDayFrom > 0) _builder.Append(MsgText(MSG.FROM)).Append(FormatDay(iDayFrom));
                    if (iDayTo < MaxDay)
                    {
                        AppendSpace();
                        _builder.Append(MsgText(MSG.TO)).Append(FormatDay(iDayTo));
                    }

                    psLastMonth = null;
                }
            }
        }

        private void AppendDay(int iDay)
        {
            AppendComma();
            _builder.Append(FormatDay(iDay));
        }

        private string FormatDay(int iDay)
        {
            DateTime dtDate = DateFrom.AddDays(iDay);
            var s = MsgMonth(dtDate.Month) + ".";
            if (!DateUnique(dtDate)) s += Conversions.ToString(dtDate.Year);
            if (!string.IsNullOrEmpty(psLastMonth) && Operators.CompareString(psLastMonth, s, false) == 0)
            {
                var i = _builder.ToString().LastIndexOf(psLastMonth, StringComparison.Ordinal);
                _builder.Remove(i, psLastMonth.Length);
            }
            else
            {
                psLastMonth = s;
            }

            return $"{dtDate.Day}.{psLastMonth}";
        }

        private bool DateUnique(DateTime dtDate)
        {
            var iDay = dtDate.Day;
            var iMonth = dtDate.Month;
            var iCount = 0;
            var year = DateFrom.Year;
            var year2 = DateTo.Year;
            checked
            {
                for (var iYear = year; iYear <= year2; iYear++)
                    try
                    {
                        dtDate = new DateTime(iYear, iMonth, iDay);
                        if (DateTime.Compare(dtDate, DateFrom) >= 0 && DateTime.Compare(dtDate, DateTo) <= 0) iCount++;
                    }
                    catch (Exception)
                    {
                        // ignored
                    }

                return iCount <= 1;
            }
        }

        private void AppendComma()
        {
            var iLast = checked(_builder.Length - 1);
            if (iLast > 0 && _builder[iLast] != ' ' && _builder[iLast] != ',') _builder.Append(',');
        }

        private void AppendSpace()
        {
            var iLast = checked(_builder.Length - 1);
            if (iLast > 0 && _builder[iLast] > ' ') _builder.Append(' ');
        }

        private void TxtParse()
        {
            var dtFrom = new DateTime();
            var dtTo = new DateTime();
            DAYTYPE iDays = 0;

            var iLevel = LEVEL.RUNS;
            var iDateLevel = DATELEVEL.DATE;
            checked
            {
                while (SkipWhiteSpace())
                {
                    var s = ExtractToken();
                    ref var ptr = ref _position;
                    var exit = false;
                    while (Operators.CompareString(s, ",", false) != 0)
                    {
                        if (TokenIsMsg(s, MSG.AND))
                        {
                            FlushData(iLevel, ref dtFrom, ref dtTo, ref iDays, ref iDateLevel, true);
                        }
                        else if (TokenIsMsg(s, MSG.RUNS, MSG.RUNS_ALT))
                        {
                            FlushData(iLevel, ref dtFrom, ref dtTo, ref iDays, ref iDateLevel, false);
                            iLevel = LEVEL.RUNS;
                            iDateLevel = DATELEVEL.DATE;
                        }
                        else if (TokenIsMsg(s, MSG.RUNSNOT, MSG.RUNSNOT_ALT))
                        {
                            FlushData(iLevel, ref dtFrom, ref dtTo, ref iDays, ref iDateLevel, false);
                            iLevel = LEVEL.RUNSNOT;
                            iDateLevel = DATELEVEL.DATE;
                        }
                        else if (TokenIsMsg(s, MSG.FROM))
                        {
                            iDateLevel = DATELEVEL.FROM;
                        }
                        else if (TokenIsMsg(s, MSG.TO))
                        {
                            iDateLevel = DATELEVEL.TO;
                        }
                        else if (TokenIsMsg(s, MSG.ON))
                        {
                            iDateLevel = DATELEVEL.ON;
                            iDays = DAYTYPE.NONE;
                        }
                        else if (iDateLevel == DATELEVEL.ON)
                        {
                            iDays |= GetDayType(s);
                        }
                        else
                        {
                            var dtLastDate = new DateTime();
                            if (IsDayType(s) || IsDayRange(s))
                            {
                                iDateLevel = DATELEVEL.ON;
                                iDays = DAYTYPE.NONE;
                                continue;
                            }

                            dtLastDate = GetDate(s, dtLastDate, iDateLevel == DATELEVEL.TO);
                            if (iDateLevel != DATELEVEL.FROM)
                            {
                                if (iDateLevel != DATELEVEL.TO)
                                {
                                    dtFrom = dtLastDate;
                                    dtTo = dtLastDate;
                                }
                                else
                                    dtTo = dtLastDate;
                            }
                            else
                                dtFrom = dtLastDate;
                        }

                        ptr = ref _position;
                        _position = ptr + s.Length;
                        exit = true;
                        break;
                    }

                    if (exit) continue;

                    if (iDateLevel != DATELEVEL.ON)
                    {
                        FlushData(iLevel, ref dtFrom, ref dtTo, ref iDays, ref iDateLevel, false);
                        ptr = ref _position;
                        _position = ptr + s.Length;
                        continue;
                    }

                    var s2 = "??";
                    var i = _position;
                    ptr = ref _position;
                    _position = ptr + s.Length;
                    if (SkipWhiteSpace()) s2 = ExtractToken();
                    _position = i;
                    if (s2.Length > 1 && (s2.IndexOf('-') < 0 || s2.Length != 3))
                    {
                        FlushData(iLevel, ref dtFrom, ref dtTo, ref iDays, ref iDateLevel, false);
                        ptr = ref _position;
                        _position = ptr + s.Length;
                        continue;
                    }

                    ptr = ref _position;
                    _position = ptr + s.Length;
                }

                FlushData(iLevel, ref dtFrom, ref dtTo, ref iDays, ref iDateLevel, false);
                FlushData(iLevel);
            }
        }

        private void FlushData(LEVEL iLevel, ref DateTime dtFrom, ref DateTime dtTo, ref DAYTYPE iDays,
            ref DATELEVEL iDateLevel, bool bAnd)
        {
            checked
            {
                if (DateTime.Compare(dtFrom, DateTime.MinValue) != 0 ||
                    DateTime.Compare(dtTo, DateTime.MinValue) != 0 || iDays != DAYTYPE.NONE)
                {
                    var o = new ParseData
                    {
                        dtFrom = dtFrom,
                        dtTo = dtTo,
                        iLevel = iLevel,
                        iDays = iDays,
                        bAnd = bAnd
                    };
                    if (iDays != DAYTYPE.NONE)
                    {
                        var i = _parsedData.Count - 1;
                        while (i >= 0 && _parsedData[i].bAnd && _parsedData[i].iDays == DAYTYPE.NONE)
                        {
                            _parsedData[i].iDays = iDays;
                            i += -1;
                        }
                    }

                    _parsedData.Add(o);
                    dtFrom = DateTime.MinValue;
                    dtTo = DateTime.MinValue;
                    iDays = DAYTYPE.NONE;
                    iDateLevel = DATELEVEL.DATE;
                }
            }
        }

        private void FlushData(LEVEL iLevel)
        {
            if (_parsedData.Count == 0)
            {
                DateTime dtFrom = DateFrom;
                var dtTo = new DateTime();
                DAYTYPE iDays = 0;
                DATELEVEL iDateLevel = 0;
                FlushData(iLevel, ref dtFrom, ref dtTo, ref iDays, ref iDateLevel, false);
            }

            checked
            {
                for (var i = 0; i < _parsedData.Count; i++)
                {
                    ParseData parseData = _parsedData[i];
                    if (i == 0 && parseData.iLevel == LEVEL.RUNSNOT)
                    {
                        for (var j = 0; j <= MaxDay; j++) 
                            _bits[j] = true;
                    }

                    if (parseData.dtFrom == DateTime.MinValue) 
                        parseData.dtFrom = DateFrom;
                    if (parseData.dtTo == DateTime.MinValue) 
                        parseData.dtTo = DateTo;

                    for (var k = DateDiff(DateFrom, parseData.dtFrom); k <= DateDiff(DateFrom, parseData.dtTo); k++)
                        if (k >= 0 && k <= MaxDay && (parseData.iDays == DAYTYPE.NONE || (GetDayType(k, true) & parseData.iDays) != DAYTYPE.NONE))
                            _bits[k] = parseData.iLevel == LEVEL.RUNS;
                }
            }
        }

        private bool SkipWhiteSpace()
        {
            while (_position < _text.Length && _text[_position] <= ' ')
            {
                _position += 1;
            }

            return _position < _text.Length;
        }

        private string ExtractToken()
        {
            checked
            {
                var iPos = _position + 1;
                while (iPos < _text.Length && _text[iPos] > ' ' && _text[iPos] != ',' && _text[_position] != ',') iPos++;
                return _text.Substring(_position, iPos - _position);
            }
        }

        private static bool TokenIsMsg(string sToken, params MSG[] aiMsg)
        {
            var i = 0;
            checked
            {
                while (i < aiMsg.Length)
                {
                    MSG iMsg = aiMsg[i];
                    if (string.Compare(sToken, MsgText(iMsg).Trim(), StringComparison.OrdinalIgnoreCase) != 0)
                    {
                        var s = messagesRegex[(int) iMsg];
                        if (string.IsNullOrEmpty(s) || sToken.Contains(" ") && !s.Contains(" ") || !Regex.Match(sToken, s).Success)
                        {
                            i++;
                            continue;
                        }
                    }

                    return true;
                }

                return false;
            }
        }

        private static bool IsDayType(string sToken) => sToken.ToUpper().TrimEnd(pconstDayType.ToCharArray()).Length == 0;

        private static bool IsDayRange(string sToken) => sToken.Length == 3 && sToken[1] == '-' && char.IsDigit(sToken[0]) && char.IsDigit(sToken[2]);

        private DAYTYPE GetDayType(string sToken)
        {
            sToken = sToken.ToUpper();
            checked
            {
                if (!IsDayType(sToken))
                {
                    var i = pconstDayType.IndexOf(sToken[0]);
                    if (sToken.Length == 1)
                    {
                        if (i >= 0) 
                            return (DAYTYPE) (1 << i);
                    }
                    else if (sToken.Length == 3 && sToken[1] == '-' && i < 6)
                    {
                        var j = "1234567".IndexOf(sToken[2]);
                        if (j > i)
                        {
                            DAYTYPE iDayType = 0;
                            while (i <= j)
                            {
                                iDayType |= (DAYTYPE) (1 << i);
                                i++;
                            }

                            return iDayType;
                        }
                    }

                    throw new ParseException($"Chybný pevný kód dne {sToken}.", _position);
                }

                var iDayType2 = DAYTYPE.NONE;
                foreach (var c in sToken) 
                    iDayType2 |= (DAYTYPE) (1 << pconstDayType.IndexOf(c));

                return iDayType2;
            }
        }

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private DateTime GetDate(string sToken, DateTime dtLastDate, bool bCheckLast)
        {
            if (sToken.IndexOf('-') >= 0)
                throw new ParseException("Pre interval dát použite od ... do ..., nie -.", _position);
            var i = sToken.IndexOf('.');
            checked
            {
                if (i >= 0 && int.TryParse(sToken.Substring(0, i), out var iDay) && iDay > 0 && iDay <= 31)
                {
                    string s;
                    if (i + 1 < sToken.Length)
                    {
                        s = sToken.Substring(i + 1);
                    }
                    else
                    {
                        s = _text.Substring(_position + sToken.Length);
                        Match oMatch = Regex.Match(s, "\\.[0-9IXV]+\\.");
                        if (oMatch.Success)
                            s = oMatch.Index + oMatch.Value.Length + 4 > s.Length
                                ? oMatch.Value.Substring(1)
                                : s.Substring(oMatch.Index + 1, oMatch.Value.Length + 3);
                        else
                            s = "";
                    }

                    var j = s.IndexOf('.');
                    if (j >= 0)
                    {
                        DateTime dtLastDateOrig = dtLastDate;
                        var iMonth = GetMonth(s.Substring(0, j));
                        var bYearSet = int.TryParse(s.Substring(j + 1), out var iYear) && iYear >= 2000 && iYear < 2100;
                        if (!bYearSet)
                        {
                            if (DateTime.Compare(dtLastDate, DateTime.MinValue) == 0) dtLastDate = DateFrom;
                            if (iMonth < dtLastDate.Month || iMonth == dtLastDate.Month && iDay < dtLastDate.Day)
                                iYear = dtLastDate.Year + 1;
                            else
                                iYear = dtLastDate.Year;
                        }

                        DateTime dtDate;
                        try
                        {
                            dtDate = new DateTime(iYear, iMonth, iDay);
                        }
                        catch (Exception)
                        {
                            throw new ParseException($"Chybný dátum {sToken}.", _position);
                        }

                        if (DateTime.Compare(dtDate, DateTo) > 0 && !bYearSet)
                        {
                            if (bCheckLast)
                            {
                                if (pbSkipDateRangeCheck) return DateTo;
                                throw new ParseException(
                                    $"Koncový dátum {FormatDate(dtDate)} je mimo rozsahu platnosti grafikonu.", _position);
                            }

                            try
                            {
                                dtDate = new DateTime(iYear - 1, iMonth, iDay);
                            }
                            catch (Exception)
                            {
                                throw new ParseException($"Chybný dátum {sToken}.", _position);
                            }
                        }

                        if (DateTime.Compare(dtDate, DateFrom) < 0 || DateTime.Compare(dtDate, DateTo) > 0)
                        {
                            if (!pbSkipDateRangeCheck)
                                throw new ParseException($"Dátum {FormatDate(dtDate)} je mimo rozsahu platnosti grafikonu.", _position);

                            if (dtLastDateOrig == DateTime.MinValue && !bYearSet && dtDate < DateFrom &&
                                DateFrom.Subtract(dtDate).TotalDays > dtDate.AddYears(1).Subtract(DateTo).TotalDays)
                                dtDate = dtDate.AddYears(1);
                        }

                        return dtDate;
                    }
                }

                throw new ParseException($"Chybný dátum {sToken}.", _position);
            }
        }

        private int GetMonth(string sMonth)
        {
            if (int.TryParse(sMonth, out var iMonth))
                return iMonth;

            var i = Array.IndexOf(messagesCZ, sMonth.ToUpper(), 23);
            if (i < 23 || i > 34) 
                throw new ParseException($"Neplatný mesiac {sMonth}.", _position);
            return checked(i - 23 + 1);
        }

        private string MsgMonth(int iMonth) => pbMonthRoman ? MsgText(checked(MSG.JAN + iMonth - 1)) : iMonth.ToString();

        private string MsgDayType(MSG iMsg)
        {
            string MsgDayType;
            if (pbInsertMarks)
            {
                ref var ptr = ref piDecorLength;
                piDecorLength = checked(ptr + 2);
                MsgDayType = "{" + MsgText(iMsg) + "}";
            }
            else
            {
                MsgDayType = MsgText(iMsg);
            }

            return MsgDayType;
        }

        private DAYTYPE GetDayType(DateTime dtDate, bool bForceSpecDays = false)
        {
            var iDayType = (DAYTYPE) (1 << (int) GetDayIndex(dtDate));
            if (pbSpecDays || bForceSpecDays)
            {
                if (IsHoliday(dtDate))
                    iDayType |= DAYTYPE.HOLIDAY;
                else if (iDayType <= DAYTYPE.FRIDAY)
                    iDayType |= DAYTYPE.WORKDAY;
            }

            return iDayType;
        }

        private DAYTYPE GetDayType(int iDay, bool bForceSpecDays = false) => GetDayType(DateFrom.AddDays(iDay), bForceSpecDays);

        private static DAYTYPE GetDayType(IReadOnlyList<int> aiOKCount)
        {
            var i = DAYINDEX.MONDAY;
            DAYTYPE iDayType = 0;
            do
            {
                if (aiOKCount[(int) i] != 0) iDayType |= (DAYTYPE) (1 << (int) i);
                i++;
            } while (i <= DAYINDEX.HOLIDAY);

            return iDayType;
        }

        private DAYTYPE CheckDayType(int iDayFrom, int iDayTo, DAYTYPE iDayType)
        {
            checked
            {
                while (iDayFrom <= iDayTo)
                {
                    if ((GetDayType(iDayFrom) & iDayType) == DAYTYPE.NONE) return iDayType;
                    iDayFrom++;
                }

                return DAYTYPE.NONE;
            }
        }

        private static DAYINDEX GetDayIndex(DateTime dtDate) => (DAYINDEX) dtDate.AddDays(-1.0).DayOfWeek;

        private DAYINDEX GetDayIndex(int iDay, int iIsFC)
        {
            DateTime dtDate = DateFrom.AddDays(iDay);
            DAYINDEX iIndex = GetDayIndex(dtDate);
            if (iIsFC == 0 || !pbSpecDays)
                return iIndex;
            if (iIndex == DAYINDEX.SATURDAY && (iIsFC & 32) != 0)
                return iIndex;
            if (IsHoliday(dtDate))
                return DAYINDEX.HOLIDAY;
            return iIndex == DAYINDEX.SATURDAY ? iIndex : DAYINDEX.WORKDAY;
        }

        private static DAYINDEX GetNextDayIndex(DAYINDEX iDay)
        {
            if (iDay > DAYINDEX.SATURDAY)
                return DAYINDEX.MONDAY;

            return checked(iDay + 1);
        }

        public static bool IsHoliday(DateTime dtDate)
        {
            if (dtDate.DayOfWeek == DayOfWeek.Sunday)
            {
                return true;
            }

            var d = dtDate.Day;
            var m = dtDate.Month;
            switch (Loc)
            {
                case LOCALE.CZECH:
                    if (d == 1 && m == 1 || d == 1 && m == 5 || d == 8 && m == 5 ||
                        d == 5 && m == 7 || d == 6 && m == 7 || d == 28 && m == 9 ||
                        d == 28 && m == 10 || d == 17 && m == 11 || d == 24 && m == 12 ||
                        d == 25 && m == 12 || d == 26 && m == 12) 
                        return true;
                    break;
                case LOCALE.SLOVAK:
                    if (d == 1 && m == 1 || d == 6 && m == 1 || d == 1 && m == 5 ||
                        d == 8 && m == 5 || d == 5 && m == 7 || d == 29 && m == 8 ||
                        d == 1 && m == 9 || d == 15 && m == 9 || d == 1 && m == 11 ||
                        d == 17 && m == 11 || d == 24 && m == 12 || d == 25 && m == 12 ||
                        d == 26 && m == 12) 
                        return true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (dtDate.DayOfWeek == DayOfWeek.Friday && (m == 3 || m == 4))
            {
                GetEasterMonday(dtDate.Year, out var iDay3, out var iMonth3);
                return dtDate.AddDays(3.0).Day == iDay3 && dtDate.AddDays(3.0).Month == iMonth3;
            }

            if (dtDate.DayOfWeek == DayOfWeek.Monday && (m == 3 || m == 4))
            {
                GetEasterMonday(dtDate.Year, out var iDay4, out var iMonth4);
                return d == iDay4 && m == iMonth4;
            }

            return false;
        }

        private static void GetEasterMonday(int year, out int month, out int day)
        {
            var a = year % 19;
            var b = year / 100;
            var num = year % 100;
            var d = b / 4;
            var e = b % 4;
            var f = num / 4;
            var g = num % 4;
            checked
            {
                var h = (8 * b + 13) / 25;
                var i = (19 * a + b - d - h + 15) % 30;
                var j = (a + 11 * i) / 319;
                var k = (2 * e + 2 * f - g - i + j + 32) % 7;
                month = (i - j + k + 91) / 25;
                day = (i - j + k + 20 + month) % 32;
            }
        }

        public class ParseException : Exception
        {
            public ParseException(string sMessage, int iPos) : base(sMessage)
            {
                Position = iPos;
            }

            public int Position { get; }
        }

        private enum LEVEL
        {
            UNDEF,
            RUNS,
            RUNSNOT
        }

        private enum DATELEVEL
        {
            DATE,
            FROM,
            TO,
            ON
        }

        private class DateRemInfo
        {
            public List<DateRemInfo> aoRuns;

            public List<DateRemInfo> aoRunsNot;

            public DAYTYPE type;

            public int from;

            public int to;

            public DateRemInfo()
            {
                aoRuns = new List<DateRemInfo>();
                aoRunsNot = new List<DateRemInfo>();
            }

            public DateRemInfo(int dayFrom, int dayTo)
            {
                aoRuns = new List<DateRemInfo>();
                aoRunsNot = new List<DateRemInfo>();
                from = dayFrom;
                to = dayTo;
            }

            public bool AllSet =>
                (type & DAYTYPE.ALL1) == DAYTYPE.ALL1 || (type & DAYTYPE.ALL2) == DAYTYPE.ALL2;

            public bool HaveDays => !AllSet && type > DAYTYPE.NONE;

            public bool Runs => aoRuns.Count > 0;

            public bool RunsNot => aoRunsNot.Count > 0;

            public bool Merge(DateRemInfo oRem)
            {
                checked
                {
                    if (HaveDays && type == oRem.type && to + 60 > oRem.from &&
                        (Runs || oRem.Runs || RunsNot || oRem.RunsNot) ||
                        !oRem.HaveDays && oRem.from == 0 && oRem.to == 0)
                    {
                        if (oRem.to != 0 || oRem.from != 0)
                        {
                            if (Runs)
                                foreach (DateRemInfo o in aoRuns)
                                    if (o.from > to && o.from < oRem.from || o.to > to && o.to < oRem.from)
                                        return false;

                            aoRunsNot.Add(new DateRemInfo(to + 1, oRem.from - 1));
                            to = oRem.to;
                        }

                        if (oRem.Runs)
                        {
                            if (Runs)
                                aoRuns.AddRange(oRem.aoRuns);
                            else
                                aoRuns = oRem.aoRuns;
                        }

                        if (oRem.RunsNot)
                        {
                            if (RunsNot)
                                aoRunsNot.AddRange(oRem.aoRunsNot);
                            else
                                aoRunsNot = oRem.aoRunsNot;
                        }

                        return true;
                    }

                    return false;
                }
            }
        }

        private class ParseData
        {
            public bool bAnd;

            public DateTime dtFrom;

            public DateTime dtTo;

            public DAYTYPE iDays;

            public LEVEL iLevel;

            public ParseData()
            {
                iLevel = LEVEL.RUNS;
            }
        }

#pragma warning disable IDE0079 // Odebrat nepotřebné potlačení
        [Flags]
        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private enum DAYTYPE
        {
            NONE = 0,
            MONDAY = 1,
            TUESDAY = 2,
            WEDNESDAY = 4,
            THURSDAY = 8,
            FRIDAY = 16,
            SATURDAY = 32,
            SUNDAY = 64,
            WORKDAY = 128,
            HOLIDAY = 256,
            ALL1 = 127,
            ALL2 = 416
        }

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private enum DAYINDEX
        {
            MONDAY,
            TUESDAY,
            WEDNESDAY,
            THURSDAY,
            FRIDAY,
            SATURDAY,
            SUNDAY,
            WORKDAY,
            HOLIDAY,
            MAX = MINWEEKDAYS
        }
#pragma warning restore IDE0079 // Odebrat nepotřebné potlačení
    }
}