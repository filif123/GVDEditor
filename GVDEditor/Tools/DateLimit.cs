using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.CompilerServices;

// ReSharper disable UnusedMember.Local
namespace GVDEditor.Tools
{
    /// <summary>
    ///     Trieda pre zpracovavanie bitoveho pola do poznamky a naopak.
    /// </summary>
    internal class DateLimit
    {
        /// <summary>
        ///     Jazyk generovaných datumových obmedzeni.
        /// </summary>
        public enum Locale
        {
            CZ,
            SK
        }

        /// <summary>
        ///     Kluce k textom
        /// </summary>
        public enum Message
        {
            Error,
            Empty,

            RunsDaily,
            RunsNever,
            RunsNeverAlt,
            RunsNext,
            RunsNot,
            RunsNotAlt,
            Runs,
            RunsAlt,

            From,
            To,
            And,
            On,

            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday,
            Sunday,

            Workday,
            Holiday,

            Jan,
            Feb,
            Mar,
            Apr,
            May,
            Jun,
            Jul,
            Aug,
            Sep,
            Oct,
            Nov,
            Dec
        }

        private const int MINCOUNT3 = 80;
        private const int MINCOUNT4 = 14;
        private const int MINCOUNT5 = 30;

        private const int MINWEEKDAYS = 8;

        private const string DayTypeSigns = "1234567X+";

        private static readonly string[] _messagesCZ =
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

        private static readonly string[] _messagesSK =
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

        private static readonly string[] _messagesRegex =
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

        private BitArray _bits;
        private readonly StringBuilder _builder;
        private readonly List<ParseData> _parsedData;

        private readonly bool _allowRunsDaily;
        private readonly bool _fromToday;
        private readonly bool _insertMarks;
        private readonly int _maxDays;
        private readonly bool _monthRoman;
        private readonly bool _skipDateRangeCheck;
        private readonly bool _specDays;
        
        private int _decorLength;
        private string _lastMonth;
        private int _position;
        private string _text;

        /// <summary>
        ///     Vytvori novu instanciu triedy <see cref="DateLimit"/>.
        /// </summary>
        /// <param name="from">Pociatocny datum platnosti GVD.</param>
        /// <param name="to">Koncovy datum platnosti GVD.</param>
        /// <param name="specDays">Pouzivat sviatky a pracovne dni.</param>
        /// <param name="allowRunsDaily">Vracat tiez - ide denne.</param>
        /// <param name="fromToday">Zohladnit az vzhladom ku dnesku.</param>
        /// <param name="bInsertMarks">Vkladat znacky {}.</param>
        /// <param name="maxDays">Pocet dni do buducnosti (maximalny poradovy index dna).</param>
        /// <param name="monthRoman">Cisla mesiacov rimskymi cislicami.</param>
        /// <param name="skipDateRangeCheck">Preskakovat u datumu chyby pre datum mimo grafikonu.</param>
        /// <param name="altForm">Pouzit zkrateny tvar poznamky.</param>
        /// <param name="today">Ktory datum pouzit ako dnesok (ak sa neuvedie, pouzije sa skutocny dnesok).</param>
        public DateLimit(DateTime from, DateTime to, 
            bool specDays = true, bool allowRunsDaily = false,
            bool fromToday = false, bool bInsertMarks = true, 
            int maxDays = 0, bool monthRoman = true,
            bool skipDateRangeCheck = false, bool altForm = false, 
            DateTime? today = null)
        {
            DateFrom = from;
            DateTo = to;
            AltForm = altForm;
            Today = today ?? DateTime.Today;

            _builder = new StringBuilder();
            _parsedData = new List<ParseData>();

            _specDays = specDays;
            _allowRunsDaily = allowRunsDaily;
            _fromToday = fromToday;
            _insertMarks = bInsertMarks;
            _maxDays = maxDays;
            _monthRoman = monthRoman;
            _skipDateRangeCheck = skipDateRangeCheck;

            if (DateTo < DateFrom)
                throw new Exception($"Dátum do {to} je menší ako dátum od {from}.");

            if (TotalDays > 400)
                throw new Exception($"Príliš dlhý grafikon <{from},{to}>.");
        }

        /// <summary>
        ///     Vrati jazyk generovanych poznamok.
        /// </summary>
        public static Locale Loc { get; set; } = Locale.SK;

        /// <summary>
        ///     Vrati celkovy pocet dni grafikonu.
        /// </summary>
        public int TotalDays => MaxDay + 1;

        /// <summary>
        ///     Vrati maximalny poradovy index dna.
        /// </summary>
        public int MaxDay => DateDiff(DateFrom, DateTo);

        /// <summary>
        ///     Vrati pociatocny datum.
        /// </summary>
        public DateTime DateFrom { get; private set; }

        /// <summary>
        ///     Vrati koncovy datum.
        /// </summary>
        public DateTime DateTo { get; private set; }

        /// <summary>
        ///     Obdobie platnosti ako text.
        /// </summary>
        public string TextFromTo => $"{FormatDate(DateFrom)} - {FormatDate(DateTo)}";

        /// <summary>
        ///     Vrati priznak alternativneho tvaru textu.
        /// </summary>
        public bool AltForm { get; }

        /// <summary>
        ///     Vrati datum pouzity ako "dnes".
        /// </summary>
        public DateTime Today { get; }

        /// <summary>
        ///     Vytvori bitove pole pre priznaky ide/nejde.
        /// </summary>
        public BitArray CreateBitArray() => new(TotalDays);

        /// <summary>
        ///     Formatuje datum do formatu "dd:MM:yyyy".
        /// </summary>
        public static string FormatDate(DateTime date) => date.ToString("dd.MM.yyyy");

        /// <summary>
        ///     Vrati vzdialenost medzi datumami ako <see cref="int"/>.
        /// </summary>
        public static int DateDiff(DateTime from, DateTime to)
        {
            return (int)Math.Round((to - from).TotalDays);
        }

        /// <summary>
        ///     Konvertuje bitove pole do textovej poznamky.
        /// </summary>
        /// <param name="bits">Bitove pole.</param>
        /// <param name="cycle">Posunutie vzhladom k bitovemu polu.</param>
        /// <param name="validBits"></param>
        /// <returns>
        ///     Text poznamky.
        /// </returns>
        public string BitArrayToText(BitArray bits, int cycle = 0, BitArray validBits = null)
        {
            if (bits == null || bits.Length != TotalDays)
                throw new ArgumentException(@"Bitové pole na vstupu chýba alebo neodpovedá jeho dĺžka.", nameof(bits));

            var dateFrom = DateFrom;
            var dateTo = DateTo;

            checked
            {
                try
                {
                    DateFrom = DateFrom.AddDays(cycle);
                    DateTo = DateTo.AddDays(cycle);

                    var maxIndex = MaxDay;
                    var minIndex = 0;

                    if (_maxDays > 0 && DateTo > Today.AddDays(_maxDays))
                    {
                        DateTo = Today.AddDays(_maxDays);

                        if (DateTo < DateFrom) 
                            return MsgText(Message.RunsNext);

                        maxIndex = DateDiff(DateFrom, DateTo);
                    }

                    if (_fromToday)
                    {
                        if (Today > DateTo) 
                            return AltMsgText(Message.RunsNever, Message.RunsNeverAlt);

                        if (Today > DateFrom)
                        {
                            minIndex = DateDiff(DateFrom, Today);
                            DateFrom = Today;
                        }
                    }

                    var hasData = false;

                    for (var i = minIndex + 1; i <= maxIndex; i++)
                    {
                        if (bits[i] != bits[minIndex])
                        {
                            hasData = true;
                            break;
                        }
                    }

                    if (!hasData)
                    {
                        return !bits[minIndex] ? 
                            AltMsgText(Message.RunsNever, Message.RunsNeverAlt) : 
                            MsgText(_allowRunsDaily ? Message.RunsDaily : Message.Empty);
                    }
                    else
                    {
                        var oValidBits2 = validBits;

                        if (minIndex > 0 || maxIndex != bits.Length - 1)
                        {
                            int j;
                            if (validBits != null && validBits.Count == bits.Count)
                            {
                                oValidBits2 = new BitArray(maxIndex - minIndex + 1);

                                j = 0;
                                for (var k = minIndex; k <= maxIndex; k++)
                                {
                                    oValidBits2[j] = validBits[k];
                                    j++;
                                }
                            }

                            var oBits2 = new BitArray(maxIndex - minIndex + 1);
                            j = 0;

                            for (var l = minIndex; l <= maxIndex; l++)
                            {
                                oBits2[j] = bits[l];
                                j++;
                            }

                            bits = oBits2;
                        }

                        _bits = bits;

                        var positive = BitArrayToTextInternal(false, out int infosCountPos, out int lenPos, oValidBits2);
                        var negative = BitArrayToTextInternal(true, out int infosCountNeg, out int lenNeg, oValidBits2);

                        if (lenNeg + (lenPos > 40 ? 20 : 25) < lenPos || lenNeg < lenPos && infosCountNeg < infosCountPos)
                            return negative;

                        return positive;
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
                finally
                {
                    DateFrom = dateFrom;
                    DateTo = dateTo;
                    _bits = null;
                }

                return "";
            }
        }

        /// <summary>
        ///     Konvertuje textovu poznamku do bitoveho pole.
        /// </summary>
        /// <param name="text">Text poznamky.</param>
        /// <returns>Text poznamky.</returns>
        public BitArray TextToBitArray(string text)
        {
            try
            {
                if (string.IsNullOrEmpty(text) || TokenIsMsg(text.Trim(), Message.RunsDaily, Message.Runs, Message.RunsAlt))
                    return new BitArray(TotalDays, true);

                if (TokenIsMsg(text.Trim(), Message.RunsNever, Message.RunsNeverAlt, Message.RunsNot, Message.RunsNotAlt))
                    return CreateBitArray();

                _bits = CreateBitArray();
                _text = text;
                _position = 0;
                _parsedData.Clear();

                ParseText();

                return _bits;
            }
            catch (ParseException)
            {
                throw;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        ///     Vrati, ci sa datumove obmedzenia ako texty prekryvaju.
        /// </summary>
        /// <param name="dl1">Text datumoveho obmedenia.</param>
        /// <param name="dl2">Text datumoveho obmedenia.</param>
        /// <returns></returns>
        public bool Overlap(string dl1, string dl2)
        {
            var limit = TextAnd(dl1, dl2);
            return limit != "" && limit != MsgText(Message.RunsNever) && limit != MsgText(Message.RunsNeverAlt);
        }

        /// <summary>
        ///     Logicka operacia AND medzi textami.
        /// </summary>
        public string TextAnd(params string[] texts)
        {
            BitArray arr;
            switch (texts.Length)
            {
                case > 1:
                    arr = TextToBitArray(texts[0]);
                    break;
                case 1:
                    return texts[0];
                default:
                    return "";
            }

            for (var i = 1; i < texts.Length; i++)
                arr = arr.And(TextToBitArray(texts[i]));

            return BitArrayToText(arr);
        }

        /// <summary>
        ///     Logicka operacia OR medzi textami.
        /// </summary>
        public string TextOr(params string[] texts)
        {
            BitArray arr;
            switch (texts.Length)
            {
                case > 1:
                    arr = TextToBitArray(texts[0]);
                    break;
                case 1:
                    return texts[0];
                default:
                    return "";
            }

            for (var i = 1; i < texts.Length; i++)
                arr = arr.Or(TextToBitArray(texts[i]));

            return BitArrayToText(arr);
        }

        /// <summary>
        ///     Logicka operacia XOR medzi textami.
        /// </summary>
        public string TextXor(params string[] texts)
        {
            BitArray arr;
            switch (texts.Length)
            {
                case > 1:
                    arr = TextToBitArray(texts[0]);
                    break;
                case 1:
                    return texts[0];
                default:
                    return "";
            }

            for (var i = 1; i < texts.Length; i++)
                arr = arr.Xor(TextToBitArray(texts[i]));

            return BitArrayToText(arr);
        }

        /// <summary>
        ///     Logicka operacia NOT podla textu.
        /// </summary>
        public string TextNot(string text)
        {
            return BitArrayToText(TextToBitArray(text).Not());
        }

        /// <summary>
        ///     Vrati text správy.
        /// </summary>
        public static string MsgText(Message message)
        {
            return Loc switch
            {
                Locale.CZ => _messagesCZ[(int)message],
                Locale.SK => _messagesSK[(int)message],
                _ => "?"
            };
        }

        /// <summary>
        ///     Text správy so zohlednenim alternativnej formulacie.
        /// </summary>
        public string AltMsgText(Message msg1, Message msg2)
        {
            return MsgText(AltForm ? msg2 : msg1);
        }

        private static void ReduceDates(IList<DateLimitInfo> dateLimit, BitArray validBits)
        {
            for (var i = dateLimit.Count - 1; i >= 0; i += -1)
            {
                var info = dateLimit[i];

                if (info.ListRuns is { Count: > 0 })
                    ReduceDates(info.ListRuns, validBits);

                if (info.ListRunsNot is { Count: > 0 })
                    ReduceDates(info.ListRunsNot, validBits);

                if (info.Type == DayType.None && info.From > 0 && info.From == info.To && !validBits[info.From])
                    dateLimit.RemoveAt(i);
            }
        }

        private string BitArrayToTextInternal(bool isNot, out int infosCount, out int length, BitArray validBits)
        {
            infosCount = 0;
            length = 0;

            if (isNot)
                _bits = _bits.Not();

            try
            {
                var limitInfos = ProcessInterval(MINCOUNT3, 0, MaxDay);

                if (validBits != null && validBits.Length == _bits.Length && limitInfos is { Count: > 0 })
                    ReduceDates(limitInfos, validBits);

                if (limitInfos != null)
                {
                    infosCount = limitInfos.Count;
                    _decorLength = 0;
                    var formatted = Format(limitInfos, isNot);

                    if (string.IsNullOrEmpty(formatted))
                        length = 0;
                    else
                    {
                        if (AltForm)
                        {
                            if (formatted.StartsWith(MsgText(Message.RunsAlt)))
                                formatted = formatted.Substring(MsgText(Message.RunsAlt).Length);

                            formatted = formatted.Replace(MsgText(Message.RunsAlt) + MsgText(Message.On), MsgText(Message.RunsAlt));
                            formatted = formatted.Replace(MsgText(Message.RunsNotAlt) + MsgText(Message.On), MsgText(Message.RunsNotAlt));
                        }

                        length = formatted.Length - _decorLength;
                    }

                    return formatted;
                }
            }
            finally
            {
                if (isNot)
                    _bits = _bits.Not();
            }

            return null;
        }

        private List<DateLimitInfo> ProcessInterval(int minCount, int from, int to)
        {
            ReduceInterval(ref from, ref to);
            var datelimit = GetSingleDays(from, to);

            if (datelimit != null)
                return datelimit;

            var OKCount = new int[9];
            var badCount = new int[9];

            var isFC = 0;
            var scanWeekDaysOK = false;

            if (ScanDays(from, to, OKCount, badCount, ref isFC) && !AllSet(OKCount) && !NoBad(badCount) && to - from > 6)
            {
                scanWeekDaysOK = true;
            }
            else
            {
                datelimit = GetIntervals(from, to);

                if (datelimit != null)
                    return datelimit;

                goto IL_74;
            }

            IL_B8:
            while (true)
            {
                datelimit = ScanWeekDays(minCount, from, to);
                if (datelimit != null)
                    break;

                var i = minCount >> 1;

                if (i <= 0)
                    i = 1;

                minCount -= i;

                if (!scanWeekDaysOK)
                    goto IL_74;
            }

            return datelimit;

            IL_74:
            datelimit = Recurse1(minCount, from, to);
            if (datelimit != null)
                return datelimit;

            datelimit = Recurse2(minCount, from, to);
            if (datelimit != null)
                return datelimit;

            datelimit = Recurse3(minCount, from, to);
            if (datelimit != null)
                return datelimit;

            datelimit = Recurse4(minCount, from, to);
            if (datelimit == null)
                goto IL_B8;

            return datelimit;
        }

        private List<DateLimitInfo> Recurse1(int minCount, int from, int to)
        {
            List<DateLimitInfo> daterem = null;
            var i = -1;

            for (var j = from; j <= to; j++)
            {
                if (Runs(j))
                {
                    if (i < 0)
                        i = j;
                }
                else
                {
                    var l = 0;
                    l++;

                    if (l >= minCount && i >= 0)
                    {
                        AppendIntervals(ref daterem, ProcessInterval(minCount, i, 0));
                        i = -1;
                    }
                }
            }

            if (i > from && daterem != null)
                AppendIntervals(ref daterem, ProcessInterval(minCount, i, to));

            return daterem;
        }

        private List<DateLimitInfo> Recurse2(int minCount, int from, int to)
        {
            List<DateLimitInfo> datelimit = null;
            var i = -1;
            var k = 0;

            for (var j = from; j <= to; j++)
            {
                if (Runs(j))
                {
                    k++;
                    if (i < 0)
                        i = j;
                }
                else
                {
                    if (k >= MINCOUNT5)
                    {
                        if (i > from)
                            datelimit = ProcessInterval(minCount, from, i - 1);

                        AppendInterval(ref datelimit, new DateLimitInfo(i, j - 1));
                        AppendIntervals(ref datelimit, ProcessInterval(minCount, j, to));

                        return datelimit;
                    }

                    k = 0;
                    i = -1;
                }
            }

            return null;
        }

        private List<DateLimitInfo> Recurse3(int minCount, int from, int to)
        {
            List<DateLimitInfo> datelimit = null;
            var i = from;
            var j = 0;

            while (i <= to && Runs(i))
            {
                j++;
                i++;
            }

            if (j >= MINCOUNT4)
            {
                AppendInterval(ref datelimit, new DateLimitInfo(from, from + j - 1));
                AppendIntervals(ref datelimit, ProcessInterval(minCount, from + j, to));

                return datelimit;
            }

            return null;
        }

        private List<DateLimitInfo> Recurse4(int minCount, int from, int to)
        {
            var i = to;
            var j = 0;

            while (i >= from && Runs(i))
            {
                j++;
                i--;
            }

            if (j >= MINCOUNT4)
            {
                var datelimit = ProcessInterval(minCount, from, to - j);
                AppendInterval(ref datelimit, new DateLimitInfo(to - j + 1, to));
                return datelimit;
            }

            return null;
        }

        private List<DateLimitInfo> ScanWeekDays(int minCount, int from, int to)
        {
            List<DateLimitInfo> aodatelimit = null;
            var OKCount = new int[9];
            var badCount = new int[9];

            checked
            {
                for (var t = to; t >= from + 7; t--)
                {
                    if (t >= to || !RunsNot(t))
                    {
                        var isFC = 0;

                        ScanDays(from, t, OKCount, badCount, ref isFC);

                        var i = 0;
                        var isOK = false;

                        do
                        {
                            if (OKCount[i] > badCount[i] * 2 && badCount[i] <= 8)
                            {
                                OKCount[i] = 1;
                                isOK = true;
                            }
                            else
                                OKCount[i] = 0;

                            i++;
                        } while (i <= 8);

                        if (isOK)
                        {
                            var j = 0;

                            for (i = from; i <= t; i++)
                                if (Runs(i) && OKCount[(int)GetDayIndex(i, isFC)] == 0 &&
                                    (i == from || RunsNot(i - 1) || OKCount[(int)GetDayIndex(i - 1, isFC)] != 0))
                                    j++;

                            var k = 0;
                            i = from;

                            while (i <= t)
                                if (OKCount[(int)GetDayIndex(i, isFC)] != 0 && RunsNot(i))
                                {
                                    k++;
                                    while (i <= t)
                                    {
                                        if (!RunsNot(i)) 
                                            break;
                                        i++;
                                    }
                                }
                                else
                                    i++;

                            if (j + k <= 13 || t - from > 350 && j + k <= 19)
                            {
                                if (k > 0)
                                {
                                    i = t;

                                    while (OKCount[(int)GetDayIndex(i, isFC)] == 0) 
                                        i--;

                                    if (RunsNot(i)) 
                                        continue;
                                }

                                if (t < to || GetBetterDayTo(t, OKCount, isFC) < MaxDay)
                                {
                                    i = 0;
                                    while (!((OKCount[(int)GetDayIndex(t - i, isFC)] != 0) ^ Runs(t - i)))
                                    {
                                        i++;
                                        if (i > 6) 
                                            goto IL_18F;
                                    }

                                    goto IL_4CB;
                                }

                                IL_18F:
                                if ((j != 0 || k != 0) && t < to - 21 && t > 13 &&
                                    (EqualPattern(t - 6, t + 1) && EqualPattern(t - 6, t + 8) &&
                                     EqualPattern(t - 6, t + 15) ||
                                     EqualPattern(t - 13, t + 1) && EqualPattern(t - 13, t + 8) &&
                                     EqualPattern(t - 13, t + 15)))
                                    continue;
                                if (j + k > 3 && t - from > 35)
                                {
                                    var aiOKCount2 = new int[9];
                                    var aiBadCount2 = new int[9];
                                    var iIsFC2 = 0;

                                    if (ScanDays(from, from + 20, aiOKCount2, aiBadCount2, ref iIsFC2) &&
                                        GetDayType(OKCount) != GetDayType(aiOKCount2))
                                    {
                                        i = from + 20;
                                        while (i <= t - 1 && ScanDays(from, i, aiOKCount2, aiBadCount2, ref iIsFC2))
                                            i++;
                                        t = i - 1;
                                        AppendIntervals(ref aodatelimit, ProcessInterval(minCount, from, t));
                                        goto IL_4CB;
                                    }
                                }

                                var _DayFrom = GetBetterDayFrom(from, OKCount, isFC);
                                var _DayTo = GetBetterDayTo(t, OKCount, isFC);

                                if (_DayFrom > 0 && OKCount[(int)GetDayIndex(_DayFrom, isFC)] == 0)
                                {
                                    i = _DayFrom;
                                    while (Runs(_DayFrom)) _DayFrom++;
                                    if (_DayFrom > i)
                                    {
                                        AppendIntervals(ref aodatelimit, ProcessInterval(minCount, i, _DayFrom - 1));
                                        while (RunsNot(_DayFrom)) _DayFrom++;
                                        from = _DayFrom;
                                    }
                                }

                                var o = new DateLimitInfo(_DayFrom, _DayTo);
                                if (_DayFrom != _DayTo)
                                    o.Type = CheckDayType(_DayFrom, _DayTo, GetDayType(OKCount));

                                AppendInterval(ref aodatelimit, o);

                                if (j > 0)
                                {
                                    i = from;
                                    while (i <= t)
                                        if (OKCount[(int)GetDayIndex(i, isFC)] == 0 && Runs(i))
                                        {
                                            j = i;
                                            while (i <= t && Runs(i)) i++;
                                            var l = i - 1;
                                            while (OKCount[(int)GetDayIndex(l, isFC)] != 0) l--;
                                            AppendPeriod(
                                                ref aodatelimit[aodatelimit.Count - 1].ListRuns, j, l);
                                        }
                                        else
                                        {
                                            i++;
                                        }
                                }

                                if (k > 0)
                                {
                                    i = from;

                                    while (i <= t)
                                        if (OKCount[(int)GetDayIndex(i, isFC)] != 0 && RunsNot(i))
                                        {
                                            j = i;
                                            k = i;
                                            var iPocetOKDnu = 0;
                                            var aiOKDen = new int[4];
                                            while (i <= t && RunsNot(i))
                                            {
                                                if (OKCount[(int)GetDayIndex(i, isFC)] != 0)
                                                {
                                                    k = i;
                                                    if (iPocetOKDnu < 2)
                                                        aiOKDen[iPocetOKDnu] = k;

                                                    iPocetOKDnu++;
                                                }

                                                i++;
                                            }

                                            if (iPocetOKDnu < 3)
                                            {
                                                var num6 = iPocetOKDnu - 1;
                                                for (k = 0; k <= num6; k++)
                                                    AppendDay(ref aodatelimit[aodatelimit.Count - 1].ListRunsNot, aiOKDen[k]);
                                            }
                                            else
                                            {
                                                while (j >= _DayFrom && RunsNot(j)) j--;
                                                j++;
                                                while (k <= _DayTo && RunsNot(k)) k++;
                                                k--;
                                                AppendPeriod(ref aodatelimit[aodatelimit.Count - 1].ListRunsNot, j, k);
                                                if (k > i)
                                                    i = k;
                                            }
                                        }
                                        else
                                        {
                                            i++;
                                        }
                                }
                            }
                        }

                        IL_4CB:
                        if (aodatelimit != null)
                        {
                            if (t < to)
                                AppendIntervals(ref aodatelimit,
                                    ProcessInterval(minCount, t + 1, to));
                            break;
                        }
                    }
                }

                return aodatelimit;
            }
        }

        /// <summary>
        ///     Pokusi sa zredukovat interval <paramref name="from"/>-<paramref name="to"/>.
        /// </summary>
        /// <param name="from">zaciatok intervalu</param>
        /// <param name="to">koniec intervalu</param>
        private void ReduceInterval(ref int from, ref int to)
        {
            while (from < to)
            {
                if (!RunsNot(from))
                    break;

                from++;
            }

            while (from < to && RunsNot(to))
                to--;
        }

        /// <summary>
        ///     Vrati jednotlive intervaly datumoveho obmedzenia.
        /// </summary>
        /// <param name="from">zaciatok intervalu</param>
        /// <param name="to">koniec intervalu</param>
        /// <returns></returns>
        private List<DateLimitInfo> GetIntervals(int from, int to)
        {
            checked
            {
                var count1 = 0;
                var count2 = 0;

                for (var i = from; i < to; i++)
                    if (Runs(i) && RunsNot(i + 1))
                        count1++;
                    else if (Runs(i + 1) && RunsNot(i))
                        count2++;

                if (Runs(to))
                    count1++;
                else
                    count2++;

                List<DateLimitInfo> intervals;

                if (count1 <= 2 && count1 <= count2 || count1 == 1 && count2 == 0)
                {
                    var info = new DateLimitInfo();
                    var j = -1;

                    for (var k = from; k <= to; k++)
                        if (Runs(k))
                        {
                            if (j < 0) 
                                j = k;
                        }
                        else if (j >= 0)
                        {
                            AppendPeriod(ref info.ListRuns, j, k - 1);
                            j = -1;
                        }

                    if (j >= 0)
                        AppendPeriod(ref info.ListRuns, j, to);

                    intervals = new List<DateLimitInfo> { info };
                }
                else
                {
                    intervals = null;
                }

                return intervals;
            }
        }

        private List<DateLimitInfo> GetSingleDays(int from, int to)
        {
            var j = 0;

            for (var i = from; i <= to; i++)
                if (Runs(i))
                    j++;

            if (j == 0) 
                return null;

            var OKCount = new int[9];
            var badCount = new int[9];
            var isFC = 0;

            var daysOK = ScanDays(from, to, OKCount, badCount, ref isFC);

            if (daysOK || j <= 6 && (j == to - from + 1 || j <= to - from + 2 - j))
            {
                var limitInfo = new DateLimitInfo();

                if (daysOK && to - from > 8)
                {
                    limitInfo.From = GetBetterDayFrom(from, OKCount, isFC);
                    limitInfo.To = GetBetterDayTo(to, OKCount, isFC);
                    limitInfo.Type = GetDayType(OKCount);
                }
                else
                {
                    for (var i = from; i <= to; i++)
                        if (Runs(i))
                            AppendDay(ref limitInfo.ListRuns, i);
                }

                return new List<DateLimitInfo> { limitInfo };
            }

            return null;
        }

        /// <summary>
        ///     Prida prvky (intervaly) zoznamu <paramref name="appendIntervals"/> do zoznamu <paramref name="baseIntervals"/>.<br></br>
        ///     Ak <paramref name="baseIntervals"/> je <see langword="null"/>, priradi referenciu <paramref name="appendIntervals"/> do <paramref name="baseIntervals"/>.
        /// </summary>
        /// <param name="baseIntervals">zakladny zoznam</param>
        /// <param name="appendIntervals">zoznam na priradenie do zakladneho zoznamu</param>
        private static void AppendIntervals(ref List<DateLimitInfo> baseIntervals, List<DateLimitInfo> appendIntervals)
        {
            if (baseIntervals == null)
            {
                baseIntervals = appendIntervals;
                return;
            }

            baseIntervals.AddRange(appendIntervals);
        }

        /// <summary>
        ///     Prida prvok <paramref name="interval"/> do zoznamu <paramref name="baseIntervals"/>.<br></br>
        ///     Ak <paramref name="baseIntervals"/> je <see langword="null"/>, vytvori sa nova instancia triedy <see cref="List{DateLimitInfo}"/>.
        /// </summary>
        /// <param name="baseIntervals"></param>
        /// <param name="interval"></param>
        private static void AppendInterval(ref List<DateLimitInfo> baseIntervals, DateLimitInfo interval)
        {
            baseIntervals ??= new List<DateLimitInfo>();
            baseIntervals.Add(interval);
        }

        private static void AppendDay(ref List<DateLimitInfo> runs, int day)
        {
            AppendPeriod(ref runs, day, day);
        }

        /// <summary>
        ///     Prida periodu.
        /// </summary>
        /// <param name="runs">intervaly</param>
        /// <param name="from">ide od</param>
        /// <param name="to">ide do</param>
        private static void AppendPeriod(ref List<DateLimitInfo> runs, int from, int to)
        {
            if (from > 0 && runs.Count > 0 && runs[runs.Count - 1].To == from - 1)
            {
                runs[runs.Count - 1].To = to;
                return;
            }

            runs.Add(new DateLimitInfo(from, to));
        }

        private bool ScanDays(int from, int to, int[] OKCount, int[] badCount, ref int isFC)
        {
            Array.Clear(OKCount, 0, OKCount.Length);
            Array.Clear(badCount, 0, badCount.Length);

            var dayIndex = GetDayIndex(from, 0);

            checked
            {
                var saturdays = 0;
                var totalBad = 0;

                for (var i = from; i <= to; i++)
                {
                    if (Runs(i))
                    {
                        if (_specDays)
                        {
                            var type = GetDayType(i);
                            if ((type & DayType.Workday) != DayType.None)
                            {
                                OKCount[7]++;
                            }
                            else if ((type & DayType.Holiday) != DayType.None)
                            {
                                OKCount[8]++;

                                if (dayIndex == DayIndex.Saturday)
                                    saturdays++;
                            }
                        }

                        OKCount[(int)dayIndex]++;
                    }
                    else
                    {
                        if (_specDays)
                        {
                            var type = GetDayType(i);
                            if ((type & DayType.Workday) != DayType.None)
                            {
                                badCount[7]++;
                            }
                            else if ((type & DayType.Holiday) != DayType.None)
                            {
                                badCount[8]++;
                            }
                        }

                        badCount[(int)dayIndex]++;
                        totalBad++;
                    }

                    dayIndex = GetNextDayIndex(dayIndex);
                }

                if (totalBad != 0)
                {
                    if (_specDays)
                    {
                        if (OKCount[5] > 2 * badCount[5] && OKCount[8] < 2 * badCount[8])
                        {
                            OKCount[8] -= saturdays;
                        }

                        var j = DayIndex.Monday;
                        var count = 0;
                        var count2 = 0;
                        do
                        {
                            if (OKCount[(int)j] > 0 && OKCount[(int)j] > badCount[(int)j])
                            {
                                if (j <= DayIndex.Sunday) 
                                    count += OKCount[(int)j] - badCount[(int)j];

                                if (j is DayIndex.Saturday or DayIndex.Workday or DayIndex.Holiday)
                                    count2 += OKCount[(int)j] - badCount[(int)j];
                            }

                            unchecked
                            {
                                j++;
                            }
                        } while (j <= DayIndex.Holiday);

                        if (count2 > count)
                        {
                            isFC = 1;
                            var k = DayIndex.Monday;
                            unchecked
                            {
                                do
                                {
                                    if (k != DayIndex.Saturday)
                                    {
                                        OKCount[(int)k] = 0;
                                        badCount[(int)k] = 0;
                                    }

                                    k++;
                                } while (k <= DayIndex.Sunday);
                            }

                            if (OKCount[8] > 2 * badCount[8] && OKCount[5] < 2 * badCount[5])
                            {
                                OKCount[5] -= saturdays;
                            }
                            else
                            {
                                isFC |= 32;
                            }
                        }
                        else
                        {
                            OKCount[7] = 0;
                            badCount[7] = 0;
                            OKCount[8] = 0;
                            badCount[8] = 0;
                        }
                    }

                    var l = DayIndex.Monday;
                    unchecked
                    {
                        while (OKCount[(int)l] == 0 || badCount[(int)l] == 0)
                        {
                            l++;
                            if (l > DayIndex.Holiday)
                                return true;
                        }
                    }
                }

                return false;
            }
        }

        private int GetBetterDayFrom(int from, IReadOnlyList<int> OKCount, int isFC)
        {
            var fromSave = from;

            if (from < 7)
            {
                while (from >= 0 && (Runs(from) || OKCount[(int)GetDayIndex(from, isFC)] == 0))
                    from--;

                if (from < 0)
                {
                    from = 0;
                }
                else
                {
                    from = fromSave;
                    while (OKCount[(int)GetDayIndex(from, isFC)] == 0)
                        from++;
                }
            }

            return from;
        }

        private int GetBetterDayTo(int to, IReadOnlyList<int> OKCount, int isFC)
        {
            var to2 = to;

            if (to > MaxDay - 7)
            {
                while (to <= MaxDay && (Runs(to) || OKCount[(int)GetDayIndex(to, isFC)] == 0))
                    to++;

                if (to > MaxDay)
                {
                    to = MaxDay;
                }
                else
                {
                    to = to2;
                    while (OKCount[(int)GetDayIndex(to, isFC)] == 0)
                        to--;
                }
            }

            return to;
        }

        private static bool AllSet(IReadOnlyList<int> count)
        {
            var dayType = GetDayType(count);
            return (dayType & DayType.All1) == DayType.All1 || 
                   (dayType & DayType.All2) == DayType.All2;
        }

        private static bool NoBad(IEnumerable<int> count)
        {
            foreach (var t in count)
                if (t != 0)
                    return false;

            return true;
        }

        /// <summary>
        ///     Vrati, ci v zadany den vlak IDE.
        /// </summary>
        /// <param name="day">Den na posudenie.</param>
        /// <returns></returns>
        private bool Runs(int day)
        {
            return _bits[day];
        }

        /// <summary>
        ///     Vrati, ci v zadany den vlak NEJDE.
        /// </summary>
        /// <param name="day">Den na posudenie.</param>
        /// <returns></returns>
        private bool RunsNot(int day)
        {
            return !Runs(day);
        }

        private bool EqualPattern(int from, int to)
        {
            var i = 0;
            while (Runs(from + i) == Runs(to + i))
            {
                i++;

                if (i > 6)
                    return true;
            }

            return false;
        }

        private string Format(IList<DateLimitInfo> datelimit, bool isNot)
        {
            if (datelimit == null || datelimit.Count == 0) return MsgText(Message.Empty);

            Merge(datelimit);
            _builder.Length = 0;
            var level = Level.Undefined;

            for (var i = 0; i < datelimit.Count; i++)
            {
                if (i + 1 < datelimit.Count)
                    Format(datelimit[i], datelimit[i + 1], isNot, ref level);
                else
                    Format(datelimit[i], null, isNot, ref level);
            }

            return _builder.ToString();
        }

        private void Format(DateLimitInfo datelimit, DateLimitInfo info, bool isNot, ref Level level)
        {
            AppendComma();
            _lastMonth = null;

            if (datelimit.AllIsSet && datelimit.From == 0 && datelimit.To == MaxDay && !datelimit.RunsNot)
            {
                _builder.Append(MsgText(Message.RunsDaily));
                return;
            }

            var baseLength = _builder.Length;
            checked
            {
                if (datelimit.HaveDays || datelimit.From != 0 || datelimit.To != 0)
                {
                    AppendPeriod(datelimit.From, datelimit.To);

                    if (datelimit.HaveDays && info != null && info.Type == datelimit.Type && !datelimit.Runs && !datelimit.RunsNot)
                        _builder.Append(MsgText(Message.And));

                    else if (datelimit.HaveDays)
                        AppendDays(datelimit.Type);

                    if (_builder.Length > baseLength)
                    {
                        if (isNot)
                        {
                            if (level != Level.RunsNot)
                            {
                                _builder.Insert(baseLength, AltMsgText(Message.RunsNot, Message.RunsNotAlt));
                                level = Level.RunsNot;
                            }
                            else if (info == null && baseLength > 0 && _builder[baseLength - 1] == ',' && !datelimit.HaveDays)
                            {
                                baseLength--;
                                _builder.Remove(baseLength, 1);
                                _builder.Insert(baseLength, MsgText(Message.And));
                            }
                        }
                        else if (level != Level.Runs)
                        {
                            _builder.Insert(baseLength, AltMsgText(Message.Runs, Message.RunsAlt));
                            level = Level.Runs;
                        }
                        else if (info == null && baseLength > 0 && _builder[baseLength - 1] == ',' && !datelimit.HaveDays)
                        {
                            baseLength--;
                            _builder.Remove(baseLength, 1);
                            _builder.Insert(baseLength, MsgText(Message.And));
                        }
                    }
                }

                foreach (var t in datelimit.ListRuns)
                {
                    if (_builder.Length == baseLength)
                    {
                        if (isNot)
                        {
                            if (level != Level.RunsNot)
                            {
                                _builder.Append(AltMsgText(Message.RunsNot, Message.RunsNotAlt));
                                level = Level.RunsNot;
                            }
                        }
                        else if (level != Level.Runs)
                        {
                            _builder.Append(AltMsgText(Message.Runs, Message.RunsAlt));
                            level = Level.Runs;
                        }
                    }

                    AppendPeriod(t.From, t.To);
                }

                _lastMonth = null;
                for (var j = 0; j < datelimit.ListRunsNot.Count; j++)
                {
                    AppendComma();
                    if (j == 0)
                    {
                        if (isNot)
                        {
                            if (level != Level.Runs)
                            {
                                _builder.Append(AltMsgText(Message.Runs, Message.RunsAlt));
                                level = Level.Runs;
                            }
                        }
                        else if (level != Level.RunsNot)
                        {
                            _builder.Append(AltMsgText(Message.RunsNot, Message.RunsNotAlt));
                            level = Level.RunsNot;
                        }
                    }

                    AppendPeriod(datelimit.ListRunsNot[j].From, datelimit.ListRunsNot[j].To);
                }
            }
        }

        private static void Merge(IList<DateLimitInfo> limits)
        {
            var i = 0;

            while (i + 1 < limits.Count)
            {
                if (limits[i].Merge(limits[i + 1]))
                    limits.RemoveAt(i + 1);
                else
                    i++;
            }
        }

        private void AppendDays(DayType dayType)
        {
            AppendSpace();
            _builder.Append(MsgText(Message.On));

            if ((dayType & DayType.Workday) == DayType.Workday) 
                _builder.Append(MsgDayType(Message.Workday));

            var i = 0;
            checked
            {
                while (true)
                    if (i > 6 || (dayType & (DayType)(1 << i)) != DayType.None)
                    {
                        if (i > 6)
                            break;

                        var j = i;

                        while (j < 6 && (dayType & (DayType)(1 << (j + 1))) != DayType.None) 
                            j++;

                        if (j - i > 1)
                        {
                            AppendComma();
                            _builder.Append(MsgDayType(Message.Monday + i)).Append("-").Append(MsgDayType(Message.Monday + j));
                        }
                        else
                        {
                            while (i <= j)
                            {
                                AppendComma();
                                _builder.Append(MsgDayType(Message.Monday + i));
                                i++;
                            }
                        }

                        i = j + 1;
                        if (i > 6)
                            break;
                    }
                    else
                    {
                        i++;
                    }

                if ((dayType & DayType.Holiday) == DayType.Holiday)
                {
                    AppendComma();
                    _builder.Append(MsgDayType(Message.Holiday));
                }
            }
        }

        private void AppendPeriod(int dayFrom, int dayTo)
        {
            if (dayFrom != 0 || dayTo != MaxDay)
            {
                if (dayTo - dayFrom <= 1)
                {
                    for (var i = dayFrom; i <= dayTo; i++)
                        AppendDay(i);

                    return;
                }

                AppendComma();

                if (dayFrom > 0)
                    _builder.Append(MsgText(Message.From)).Append(FormatDay(dayFrom));

                if (dayTo < MaxDay)
                {
                    AppendSpace();
                    _builder.Append(MsgText(Message.To)).Append(FormatDay(dayTo));
                }

                _lastMonth = null;
            }
        }

        /// <summary>
        ///     Prida zadany den do <see cref="StringBuilder"/>a, ktory pred pridanim sformatuje.
        /// </summary>
        /// <param name="day">Den, ktory sa ma pridat na koniec buildera.</param>
        private void AppendDay(int day)
        {
            AppendComma();
            _builder.Append(FormatDay(day));
        }

        /// <summary>
        ///     Sformatuje zadany den.
        /// </summary>
        /// <param name="day">Den, ktory sa ma sformatovat.</param>
        /// <returns>sformatovany den ako retazec.</returns>
        private string FormatDay(int day)
        {
            var date = DateFrom.AddDays(day);
            var month = MsgMonth(date.Month) + ".";

            if (!DateUnique(date)) 
                month += date.Year;

            if (!string.IsNullOrEmpty(_lastMonth) && _lastMonth == month)
            {
                var i = _builder.ToString().LastIndexOf(_lastMonth, StringComparison.Ordinal);
                _builder.Remove(i, _lastMonth.Length);
            }
            else
                _lastMonth = month;

            return $"{date.Day}.{_lastMonth}";
        }

        private bool DateUnique(DateTime date)
        {
            var count = 0;

            for (var i = DateFrom.Year; i <= DateTo.Year; i++)
            {
                try
                {
                    date = new DateTime(i, date.Month, date.Day);

                    if (date >= DateFrom && date <= DateTo)
                        count++;
                }
                catch (Exception)
                {
                    /* ignored*/
                }
            }

            return count <= 1;
        }

        /// <summary>
        ///     Prida znak ciarky (,) na koniec <see cref="StringBuilder"/>a.
        /// </summary>
        private void AppendComma()
        {
            var last = _builder.Length - 1;
            if (last > 0 && _builder[last] != ' ' && _builder[last] != ',')
                _builder.Append(',');
        }

        /// <summary>
        ///     Prida znak medzery ( ) na koniec <see cref="StringBuilder"/>a.
        /// </summary>
        private void AppendSpace()
        {
            var last = _builder.Length - 1;
            if (last > 0 && _builder[last] > ' ')
                _builder.Append(' ');
        }

        private void ParseText()
        {
            var from = new DateTime();
            var to = new DateTime();
            DayType days = 0;
            var level = Level.Runs;
            var dateLevel = DateLevel.DATE;

            while (SkipWhiteSpace())
            {
                var token = ExtractToken();
                var exit = false;

                while (Operators.CompareString(token, ",", false) != 0)
                {
                    if (TokenIsMsg(token, Message.And))
                    {
                        FlushData(level, ref from, ref to, ref days, ref dateLevel, true);
                    }
                    else if (TokenIsMsg(token, Message.Runs, Message.RunsAlt))
                    {
                        FlushData(level, ref from, ref to, ref days, ref dateLevel, false);
                        level = Level.Runs;
                        dateLevel = DateLevel.DATE;
                    }
                    else if (TokenIsMsg(token, Message.RunsNot, Message.RunsNotAlt))
                    {
                        FlushData(level, ref from, ref to, ref days, ref dateLevel, false);
                        level = Level.RunsNot;
                        dateLevel = DateLevel.DATE;
                    }
                    else if (TokenIsMsg(token, Message.From))
                    {
                        dateLevel = DateLevel.FROM;
                    }
                    else if (TokenIsMsg(token, Message.To))
                    {
                        dateLevel = DateLevel.TO;
                    }
                    else if (TokenIsMsg(token, Message.On))
                    {
                        dateLevel = DateLevel.ON;
                        days = DayType.None;
                    }
                    else if (dateLevel == DateLevel.ON)
                    {
                        days |= GetDayType(token);
                    }
                    else
                    {
                        var lastDate = new DateTime();
                        if (IsDayType(token) || IsDayRange(token))
                        {
                            dateLevel = DateLevel.ON;
                            days = DayType.None;
                            continue;
                        }

                        lastDate = GetDate(token, lastDate, dateLevel == DateLevel.TO);
                        if (dateLevel != DateLevel.FROM)
                        {
                            if (dateLevel != DateLevel.TO)
                            {
                                from = lastDate;
                                to = lastDate;
                            }
                            else
                                to = lastDate;
                        }
                        else
                            from = lastDate;
                    }

                    _position += token.Length;
                    exit = true;
                    break;
                }

                if (exit)
                    continue;

                if (dateLevel != DateLevel.ON)
                {
                    FlushData(level, ref from, ref to, ref days, ref dateLevel, false);
                    _position += token.Length;
                    continue;
                }

                var s = "??";
                var i = _position;
                _position += token.Length;

                if (SkipWhiteSpace()) 
                    s = ExtractToken();

                _position = i;
                if (s.Length > 1 && (s.IndexOf('-') < 0 || s.Length != 3))
                {
                    FlushData(level, ref from, ref to, ref days, ref dateLevel, false);
                    _position += token.Length;
                    continue;
                }

                _position += token.Length;
            }

            FlushData(level, ref from, ref to, ref days, ref dateLevel, false);
            FlushData(level);
        }

        private void FlushData(Level level, ref DateTime from, ref DateTime to, ref DayType days, ref DateLevel dateLevel, bool and)
        {
            if (from != DateTime.MinValue || to != DateTime.MinValue || days != DayType.None)
            {
                var o = new ParseData
                {
                    From = from,
                    To = to,
                    Level = level,
                    Days = days,
                    And = and
                };

                if (days != DayType.None)
                {
                    var i = _parsedData.Count - 1;
                    while (i >= 0 && _parsedData[i].And && _parsedData[i].Days == DayType.None)
                    {
                        _parsedData[i].Days = days;
                        i += -1;
                    }
                }

                _parsedData.Add(o);

                from = DateTime.MinValue;
                to = DateTime.MinValue;
                days = DayType.None;
                dateLevel = DateLevel.DATE;
            }
        }

        private void FlushData(Level level)
        {
            if (_parsedData.Count == 0)
            {
                var from = DateFrom;
                var to = new DateTime();
                DayType days = 0;
                DateLevel dateLevel = 0;

                FlushData(level, ref from, ref to, ref days, ref dateLevel, false);
            }

            for (var i = 0; i < _parsedData.Count; i++)
            {
                var parseData = _parsedData[i];

                if (i == 0 && parseData.Level == Level.RunsNot)
                    for (var j = 0; j <= MaxDay; j++)
                        _bits[j] = true;

                if (parseData.From == DateTime.MinValue)
                    parseData.From = DateFrom;

                if (parseData.To == DateTime.MinValue)
                    parseData.To = DateTo;

                for (var k = DateDiff(DateFrom, parseData.From); k <= DateDiff(DateFrom, parseData.To); k++)
                    if (k >= 0 && k <= MaxDay && (parseData.Days == DayType.None || (GetDayType(k, true) & parseData.Days) != DayType.None))
                        _bits[k] = parseData.Level == Level.Runs;
            }
        }

        private bool SkipWhiteSpace()
        {
            while (_position < _text.Length && _text[_position] <= ' ')
                _position += 1;

            return _position < _text.Length;
        }

        private string ExtractToken()
        {
            var pos = _position + 1;

            while (pos < _text.Length && _text[pos] > ' ' && _text[pos] != ',' && _text[_position] != ',')
                pos++;

            return _text.Substring(_position, pos - _position);
        }

        private static bool TokenIsMsg(string token, params Message[] msgs)
        {
            var i = 0;

            while (i < msgs.Length)
            {
                if (!string.Equals(token, MsgText(msgs[i]).Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    var pattern = _messagesRegex[(int)msgs[i]];

                    if (string.IsNullOrEmpty(pattern) || token.Contains(" ") &&
                        !pattern.Contains(" ") || !Regex.Match(token, pattern).Success)
                    {
                        i++;
                        continue;
                    }
                }

                return true;
            }

            return false;
        }

        private static bool IsDayType(string token)
        {
            return token.ToUpper().TrimEnd(DayTypeSigns.ToCharArray()).Length == 0;
        }

        private static bool IsDayRange(string token)
        {
            return token.Length == 3 && token[1] == '-' && char.IsDigit(token[0]) && char.IsDigit(token[2]);
        }

        private DayType GetDayType(string token)
        {
            token = token.ToUpper();

            checked
            {
                if (!IsDayType(token))
                {
                    var i = DayTypeSigns.IndexOf(token[0]);

                    switch (token.Length)
                    {
                        case 1 when i >= 0:
                            return (DayType)(1 << i);
                        case 3 when token[1] == '-' && i < 6:
                        {
                            var j = "1234567".IndexOf(token[2]);
                            if (j > i)
                            {
                                DayType dayType = 0;
                                while (i <= j)
                                {
                                    dayType |= (DayType)(1 << i);
                                    i++;
                                }

                                return dayType;
                            }

                            break;
                        }
                    }

                    throw new ParseException($"Chybný pevný kód dňa {token}.", _position);
                }

                var daytype = DayType.None;

                foreach (var c in token)
                    daytype |= (DayType)(1 << DayTypeSigns.IndexOf(c));

                return daytype;
            }
        }

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private DateTime GetDate(string token, DateTime lastDate, bool checkLast)
        {
            if (token.IndexOf('-') >= 0)
                throw new ParseException("Pre interval dát použite od ... do ..., nie -.", _position);

            var i = token.IndexOf('.');

            if (i < 0 || !int.TryParse(token.Substring(0, i), out var day) || day is <= 0 or > 31)
                throw new ParseException($"Chybný dátum {token}.", _position);

            string s;
            if (i + 1 < token.Length)
            {
                s = token.Substring(i + 1);
            }
            else
            {
                s = _text.Substring(_position + token.Length);

                var oMatch = Regex.Match(s, "\\.[0-9IXV]+\\.");

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
                var dtLastDateOrig = lastDate;
                var month = GetMonth(s.Substring(0, j));
                var yearSet = int.TryParse(s.Substring(j + 1), out var year) && year is >= 2000 and < 2100;

                if (!yearSet)
                {
                    if (lastDate == DateTime.MinValue)
                        lastDate = DateFrom;

                    if (month < lastDate.Month || month == lastDate.Month && day < lastDate.Day)
                        year = lastDate.Year + 1;
                    else
                        year = lastDate.Year;
                }

                DateTime date;
                try
                {
                    date = new DateTime(year, month, day);
                }
                catch (Exception)
                {
                    throw new ParseException($"Chybný dátum {token}.", _position);
                }

                if (date > DateTo && !yearSet)
                {
                    if (checkLast)
                    {
                        if (_skipDateRangeCheck)
                            return DateTo;

                        throw new ParseException($"Koncový dátum {FormatDate(date)} je mimo rozsahu platnosti grafikonu.", _position);
                    }

                    try
                    {
                        date = new DateTime(year - 1, month, day);
                    }
                    catch (Exception)
                    {
                        throw new ParseException($"Chybný dátum {token}.", _position);
                    }
                }

                if (date < DateFrom || date > DateTo)
                {
                    if (!_skipDateRangeCheck)
                        throw new ParseException($"Dátum {FormatDate(date)} je mimo rozsahu platnosti grafikonu.", _position);

                    if (dtLastDateOrig == DateTime.MinValue && !yearSet && date < DateFrom &&
                        DateFrom.Subtract(date).TotalDays > date.AddYears(1).Subtract(DateTo).TotalDays)
                    {
                        date = date.AddYears(1);
                    }
                }

                return date;
            }

            throw new ParseException($"Chybný dátum {token}.", _position);
        }

        private int GetMonth(string month)
        {
            if (int.TryParse(month, out var monthIndex))
                return monthIndex;

            var i = Array.IndexOf(_messagesCZ, month.ToUpper(), 23);

            if (i is < 23 or > 34)
                throw new ParseException($"Neplatný mesiac {month}.", _position);

            return i - 23 + 1;
        }

        private string MsgMonth(int month)
        {
            return _monthRoman ? MsgText(Message.Jan + month - 1) : month.ToString();
        }

        private string MsgDayType(Message message)
        {
            if (_insertMarks)
            {
                _decorLength += 2;
                return "{" + MsgText(message) + "}";
            }

            return MsgText(message);
        }

        private DayType GetDayType(DateTime date, bool forceSpecDays = false)
        {
            var iDayType = (DayType)(1 << (int)GetDayIndex(date));
            if (_specDays || forceSpecDays)
            {
                if (IsHoliday(date))
                    iDayType |= DayType.Holiday;

                else if (iDayType <= DayType.Friday)
                    iDayType |= DayType.Workday;
            }

            return iDayType;
        }

        private DayType GetDayType(int day, bool forceSpecDays = false)
        {
            return GetDayType(DateFrom.AddDays(day), forceSpecDays);
        }

        private static DayType GetDayType(IReadOnlyList<int> OKCount)
        {
            var i = DayIndex.Monday;
            DayType dayType = 0;

            do
            {
                if (OKCount[(int)i] != 0)
                    dayType |= (DayType)(1 << (int)i);

                i++;
            } while (i <= DayIndex.Holiday);

            return dayType;
        }

        private DayType CheckDayType(int dayFrom, int dayTo, DayType dayType)
        {
            checked
            {
                while (dayFrom <= dayTo)
                {
                    if ((GetDayType(dayFrom) & dayType) == DayType.None)
                        return dayType;

                    dayFrom++;
                }

                return DayType.None;
            }
        }

        private static DayIndex GetDayIndex(DateTime date)
        {
            return (DayIndex)date.AddDays(-1).DayOfWeek;
        }

        private DayIndex GetDayIndex(int day, int isFC)
        {
            var date = DateFrom.AddDays(day);
            var index = GetDayIndex(date);

            if (isFC == 0 || !_specDays)
                return index;

            if (index == DayIndex.Saturday && (isFC & 32) != 0)
                return index;

            if (IsHoliday(date))
                return DayIndex.Holiday;

            return index == DayIndex.Saturday ? index : DayIndex.Workday;
        }

        /// <summary>
        ///     Vrati index nasledujuceho dna.
        ///     Ak je <paramref name="day"/> <see cref="DayIndex.Sunday"/>, vrati <see cref="DayIndex.Monday"/>.
        /// </summary>
        /// <param name="day">Index dna.</param>
        /// <returns>Index nasledujuceho dna.</returns>
        private static DayIndex GetNextDayIndex(DayIndex day)
        {
            if (day >= DayIndex.Sunday)
                return DayIndex.Monday;

            return day + 1;
        }

        public static bool IsHoliday(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Sunday)
                return true;

            var d = date.Day;
            var m = date.Month;
            switch (Loc)
            {
                case Locale.CZ:
                    if (d == 1 && m == 1 || // 1.1.
                        d == 1 && m == 5 || // 1.5.
                        d == 8 && m == 5 || // 8.5.
                        d == 5 && m == 7 || // 5.7.
                        d == 6 && m == 7 || // 6.7.
                        d == 28 && m == 9 || // 28.9.
                        d == 28 && m == 10 || // 28.10.
                        d == 17 && m == 11 || // 17.11.
                        d == 24 && m == 12 || // 24.12.
                        d == 25 && m == 12 || // 25.12.
                        d == 26 && m == 12) // 26.12.
                        return true;
                    break;
                case Locale.SK:
                    if (d == 1 && m == 1 || // 1.1.
                        d == 6 && m == 1 || // 6.1.
                        d == 1 && m == 5 || // 1.5.
                        d == 8 && m == 5 || // 8.5.
                        d == 5 && m == 7 || // 5.7. 
                        d == 29 && m == 8 || // 29.8.
                        d == 1 && m == 9 || // 1.9. 
                        d == 15 && m == 9 || // 15.9.
                        d == 1 && m == 11 || // 1.11.
                        d == 17 && m == 11 || // 17.11.
                        d == 24 && m == 12 || // 24.12.
                        d == 25 && m == 12 || // 25.12.
                        d == 26 && m == 12) // 26.12.
                        return true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            //ak mesiac nie je marec alebo april (kvoli velkej noci)
            if (m is not (3 or 4)) 
                return false;

            switch (date.DayOfWeek)
            {
                case DayOfWeek.Friday:
                {
                    GetEasterMonday(date.Year, out var day, out var month);
                    return date.AddDays(3).Day == day && date.AddDays(3).Month == month;
                }
                case DayOfWeek.Monday:
                {
                    GetEasterMonday(date.Year, out var day, out var month);
                    return d == day && m == month;
                }
                default:
                    return false;
            }
        }

        private static void GetEasterMonday(int year, out int month, out int day)
        {
            var a = year % 19;
            var b = year / 100;
            var c = year % 100;
            var d = b / 4;
            var e = b % 4;
            var f = c / 4;
            var g = c % 4;
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
            public ParseException(string message, int pos) : base(message)
            {
                Position = pos;
            }

            // ReSharper disable once UnusedAutoPropertyAccessor.Global
            public int Position { get; }
        }

        private enum Level
        {
            /// <summary>
            ///     Nedefinovane.
            /// </summary>
            Undefined,

            /// <summary>
            ///     Ide.
            /// </summary>
            Runs,

            /// <summary>
            ///     Nejde.
            /// </summary>
            RunsNot
        }

        private enum DateLevel
        {
            DATE,
            FROM,
            TO,
            ON
        }

        private class DateLimitInfo
        {
            public int From;
            public int To;
            public DayType Type;
            public List<DateLimitInfo> ListRuns;
            public List<DateLimitInfo> ListRunsNot;

            public DateLimitInfo()
            {
                ListRuns = new List<DateLimitInfo>();
                ListRunsNot = new List<DateLimitInfo>();
            }

            public DateLimitInfo(int dayFrom, int dayTo) : this()
            {
                From = dayFrom;
                To = dayTo;
            }

            public bool AllIsSet => (Type & DayType.All1) == DayType.All1 || (Type & DayType.All2) == DayType.All2;

            public bool HaveDays => !AllIsSet && Type > DayType.None;

            public bool Runs => ListRuns.Count > 0;

            public bool RunsNot => ListRunsNot.Count > 0;

            public bool Merge(DateLimitInfo info)
            {
                if (HaveDays && Type == info.Type &&
                    To + 60 > info.From &&
                    (Runs || info.Runs || RunsNot || info.RunsNot) || !info.HaveDays && 
                    info.From == 0 && 
                    info.To == 0)
                {
                    if (info.To != 0 || info.From != 0)
                    {
                        if (Runs)
                            foreach (var o in ListRuns)
                                if (o.From > To && o.From < info.From || o.To > To && o.To < info.From)
                                    return false;

                        ListRunsNot.Add(new DateLimitInfo(To + 1, info.From - 1));
                        To = info.To;
                    }

                    if (info.Runs)
                    {
                        if (Runs)
                            ListRuns.AddRange(info.ListRuns);
                        else
                            ListRuns = info.ListRuns;
                    }

                    if (info.RunsNot)
                    {
                        if (RunsNot)
                            ListRunsNot.AddRange(info.ListRunsNot);
                        else
                            ListRunsNot = info.ListRunsNot;
                    }

                    return true;
                }

                return false;
            }
        }

        private class ParseData
        {
            public bool And;
            public DayType Days;
            public DateTime From;
            public Level Level = Level.Runs;
            public DateTime To;
        }

        [Flags]
        private enum DayType
        {
            None = 0,
            Monday = 1,
            Tuesday = 2,
            Wednesday = 4,
            Thursday = 8,
            Friday = 16,
            Saturday = 32,
            Sunday = 64,
            Workday = 128,
            Holiday = 256,
            All1 = 127,
            All2 = 416
        }

        private enum DayIndex
        {
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday,
            Sunday,
            Workday,
            Holiday,
            Max = MINWEEKDAYS
        }
    }
}