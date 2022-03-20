﻿using System;
using System.Collections.Generic;
using System.Text;
using Irony.Parsing;

namespace Irony.Samples.Java
{
	partial class JavaGrammar
	{
		public static NumberLiteral CreateJavaNumber(string name)
		{
			var term = new NumberLiteral(name, NumberOptions.AllowStartEndDot)
			{
				DefaultIntTypes = new[] { TypeCode.Int32 },
				DefaultFloatType = TypeCode.Double
			};
			term.AddPrefix("0x", NumberOptions.Hex);
			term.AddSuffix("l", TypeCode.Int64);
			term.AddSuffix("f", TypeCode.Single);
			term.AddSuffix("d", TypeCode.Double);
			return term;
		}

		public static StringLiteral CreateJavaString(string name)
		{
			return new StringLiteral(name, "\"", StringOptions.AllowsAllEscapes);
		}

		public static StringLiteral CreateJavaChar(string name)
		{
			return new StringLiteral(name, "'", StringOptions.IsChar | StringOptions.AllowsAllEscapes);
		}

		public static Terminal CreateJavaNull(string name)
		{
			return new KeyTerm("null", name);
		}

		private static void InitializeCharacterSet(ref string characterSet, IEnumerable<int[]> ranges)
		{
			var sbCharSet = new StringBuilder();
			foreach (var range in ranges)
			{
				for (int i = range[0]; i <= range[1]; ++i)
				{
					sbCharSet.Append((Char)i);
				}
			}
			characterSet = sbCharSet.ToString();
		}

		private static string _validIdentifierStartCharacters;
		public static string ValidIdentifierStartCharacters
		{
			get
			{
				if (_validIdentifierStartCharacters == null)
				{
					InitializeCharacterSet(ref _validIdentifierStartCharacters, ValidIdentifierStartCharactersRanges);
				}
				return _validIdentifierStartCharacters;
			}
		}

		private static string _validIdentifierCharacters;
		public static string ValidIdentifierCharacters
		{
			get
			{
				if (_validIdentifierCharacters == null)
				{
					InitializeCharacterSet(ref _validIdentifierCharacters, ValidIdentifierCharactersRanges);
				}
				return _validIdentifierCharacters;
			}
		}

		private static readonly int[][] ValidIdentifierStartCharactersRanges = new[]
																		{
#region Identifier Start Character Ranges

											new[] {36, 36},
											new[] {65, 90},
											new[] {95, 95},
											new[] {97, 122},
											new[] {162, 165},
											new[] {170, 170},
											new[] {181, 181},
											new[] {186, 186},
											new[] {192, 214},
											new[] {216, 246},
											new[] {248, 566},
											new[] {592, 705},
											new[] {710, 721},
											new[] {736, 740},
											new[] {750, 750},
											new[] {890, 890},
											new[] {902, 902},
											new[] {904, 906},
											new[] {908, 908},
											new[] {910, 929},
											new[] {931, 974},
											new[] {976, 1013},
											new[] {1015, 1019},
											new[] {1024, 1153},
											new[] {1162, 1230},
											new[] {1232, 1269},
											new[] {1272, 1273},
											new[] {1280, 1295},
											new[] {1329, 1366},
											new[] {1369, 1369},
											new[] {1377, 1415},
											new[] {1488, 1514},
											new[] {1520, 1522},
											new[] {1569, 1594},
											new[] {1600, 1610},
											new[] {1646, 1647},
											new[] {1649, 1747},
											new[] {1749, 1749},
											new[] {1765, 1766},
											new[] {1774, 1775},
											new[] {1786, 1788},
											new[] {1791, 1791},
											new[] {1808, 1808},
											new[] {1810, 1839},
											new[] {1869, 1871},
											new[] {1920, 1957},
											new[] {1969, 1969},
											new[] {2308, 2361},
											new[] {2365, 2365},
											new[] {2384, 2384},
											new[] {2392, 2401},
											new[] {2437, 2444},
											new[] {2447, 2448},
											new[] {2451, 2472},
											new[] {2474, 2480},
											new[] {2482, 2482},
											new[] {2486, 2489},
											new[] {2493, 2493},
											new[] {2524, 2525},
											new[] {2527, 2529},
											new[] {2544, 2547},
											new[] {2565, 2570},
											new[] {2575, 2576},
											new[] {2579, 2600},
											new[] {2602, 2608},
											new[] {2610, 2611},
											new[] {2613, 2614},
											new[] {2616, 2617},
											new[] {2649, 2652},
											new[] {2654, 2654},
											new[] {2674, 2676},
											new[] {2693, 2701},
											new[] {2703, 2705},
											new[] {2707, 2728},
											new[] {2730, 2736},
											new[] {2738, 2739},
											new[] {2741, 2745},
											new[] {2749, 2749},
											new[] {2768, 2768},
											new[] {2784, 2785},
											new[] {2801, 2801},
											new[] {2821, 2828},
											new[] {2831, 2832},
											new[] {2835, 2856},
											new[] {2858, 2864},
											new[] {2866, 2867},
											new[] {2869, 2873},
											new[] {2877, 2877},
											new[] {2908, 2909},
											new[] {2911, 2913},
											new[] {2929, 2929},
											new[] {2947, 2947},
											new[] {2949, 2954},
											new[] {2958, 2960},
											new[] {2962, 2965},
											new[] {2969, 2970},
											new[] {2972, 2972},
											new[] {2974, 2975},
											new[] {2979, 2980},
											new[] {2984, 2986},
											new[] {2990, 2997},
											new[] {2999, 3001},
											new[] {3065, 3065},
											new[] {3077, 3084},
											new[] {3086, 3088},
											new[] {3090, 3112},
											new[] {3114, 3123},
											new[] {3125, 3129},
											new[] {3168, 3169},
											new[] {3205, 3212},
											new[] {3214, 3216},
											new[] {3218, 3240},
											new[] {3242, 3251},
											new[] {3253, 3257},
											new[] {3261, 3261},
											new[] {3294, 3294},
											new[] {3296, 3297},
											new[] {3333, 3340},
											new[] {3342, 3344},
											new[] {3346, 3368},
											new[] {3370, 3385},
											new[] {3424, 3425},
											new[] {3461, 3478},
											new[] {3482, 3505},
											new[] {3507, 3515},
											new[] {3517, 3517},
											new[] {3520, 3526},
											new[] {3585, 3632},
											new[] {3634, 3635},
											new[] {3647, 3654},
											new[] {3713, 3714},
											new[] {3716, 3716},
											new[] {3719, 3720},
											new[] {3722, 3722},
											new[] {3725, 3725},
											new[] {3732, 3735},
											new[] {3737, 3743},
											new[] {3745, 3747},
											new[] {3749, 3749},
											new[] {3751, 3751},
											new[] {3754, 3755},
											new[] {3757, 3760},
											new[] {3762, 3763},
											new[] {3773, 3773},
											new[] {3776, 3780},
											new[] {3782, 3782},
											new[] {3804, 3805},
											new[] {3840, 3840},
											new[] {3904, 3911},
											new[] {3913, 3946},
											new[] {3976, 3979},
											new[] {4096, 4129},
											new[] {4131, 4135},
											new[] {4137, 4138},
											new[] {4176, 4181},
											new[] {4256, 4293},
											new[] {4304, 4344},
											new[] {4352, 4441},
											new[] {4447, 4514},
											new[] {4520, 4601},
											new[] {4608, 4614},
											new[] {4616, 4678},
											new[] {4680, 4680},
											new[] {4682, 4685},
											new[] {4688, 4694},
											new[] {4696, 4696},
											new[] {4698, 4701},
											new[] {4704, 4742},
											new[] {4744, 4744},
											new[] {4746, 4749},
											new[] {4752, 4782},
											new[] {4784, 4784},
											new[] {4786, 4789},
											new[] {4792, 4798},
											new[] {4800, 4800},
											new[] {4802, 4805},
											new[] {4808, 4814},
											new[] {4816, 4822},
											new[] {4824, 4846},
											new[] {4848, 4878},
											new[] {4880, 4880},
											new[] {4882, 4885},
											new[] {4888, 4894},
											new[] {4896, 4934},
											new[] {4936, 4954},
											new[] {5024, 5108},
											new[] {5121, 5740},
											new[] {5743, 5750},
											new[] {5761, 5786},
											new[] {5792, 5866},
											new[] {5870, 5872},
											new[] {5888, 5900},
											new[] {5902, 5905},
											new[] {5920, 5937},
											new[] {5952, 5969},
											new[] {5984, 5996},
											new[] {5998, 6000},
											new[] {6016, 6067},
											new[] {6103, 6103},
											new[] {6107, 6108},
											new[] {6176, 6263},
											new[] {6272, 6312},
											new[] {6400, 6428},
											new[] {6480, 6509},
											new[] {6512, 6516},
											new[] {7424, 7531},
											new[] {7680, 7835},
											new[] {7840, 7929},
											new[] {7936, 7957},
											new[] {7960, 7965},
											new[] {7968, 8005},
											new[] {8008, 8013},
											new[] {8016, 8023},
											new[] {8025, 8025},
											new[] {8027, 8027},
											new[] {8029, 8029},
											new[] {8031, 8061},
											new[] {8064, 8116},
											new[] {8118, 8124},
											new[] {8126, 8126},
											new[] {8130, 8132},
											new[] {8134, 8140},
											new[] {8144, 8147},
											new[] {8150, 8155},
											new[] {8160, 8172},
											new[] {8178, 8180},
											new[] {8182, 8188},
											new[] {8255, 8256},
											new[] {8276, 8276},
											new[] {8305, 8305},
											new[] {8319, 8319},
											new[] {8352, 8369},
											new[] {8450, 8450},
											new[] {8455, 8455},
											new[] {8458, 8467},
											new[] {8469, 8469},
											new[] {8473, 8477},
											new[] {8484, 8484},
											new[] {8486, 8486},
											new[] {8488, 8488},
											new[] {8490, 8493},
											new[] {8495, 8497},
											new[] {8499, 8505},
											new[] {8509, 8511},
											new[] {8517, 8521},
											new[] {8544, 8579},
											new[] {12293, 12295},
											new[] {12321, 12329},
											new[] {12337, 12341},
											new[] {12344, 12348},
											new[] {12353, 12438},
											new[] {12445, 12447},
											new[] {12449, 12543},
											new[] {12549, 12588},
											new[] {12593, 12686},
											new[] {12704, 12727},
											new[] {12784, 12799},
											new[] {13312, 19893},
											new[] {19968, 40869},
											new[] {40960, 42124},
											new[] {44032, 55203},
											new[] {63744, 64045},
											new[] {64048, 64106},
											new[] {64256, 64262},
											new[] {64275, 64279},
											new[] {64285, 64285},
											new[] {64287, 64296},
											new[] {64298, 64310},
											new[] {64312, 64316},
											new[] {64318, 64318},
											new[] {64320, 64321},
											new[] {64323, 64324},
											new[] {64326, 64433},
											new[] {64467, 64829},
											new[] {64848, 64911},
											new[] {64914, 64967},
											new[] {65008, 65020},
											new[] {65075, 65076},
											new[] {65101, 65103},
											new[] {65129, 65129},
											new[] {65136, 65140},
											new[] {65142, 65276},
											new[] {65284, 65284},
											new[] {65313, 65338},
											new[] {65343, 65343},
											new[] {65345, 65370},
											new[] {65381, 65470},
											new[] {65474, 65479},
											new[] {65482, 65487},
											new[] {65490, 65495},
											new[] {65498, 65500},
											new[] {65504, 65505},
											new[] {65509, 65510},
#endregion
										};

		private static readonly int[][] ValidIdentifierCharactersRanges = new[]
									{
#region Identifier Character Ranges
										new[] {0, 8},
										new[] {14, 27},
										new[] {36, 36},
										new[] {48, 57},
										new[] {65, 90},
										new[] {95, 95},
										new[] {97, 122},
										new[] {127, 159},
										new[] {162, 165},
										new[] {170, 170},
										new[] {173, 173},
										new[] {181, 181},
										new[] {186, 186},
										new[] {192, 214},
										new[] {216, 246},
										new[] {248, 566},
										new[] {592, 705},
										new[] {710, 721},
										new[] {736, 740},
										new[] {750, 750},
										new[] {768, 855},
										new[] {861, 879},
										new[] {890, 890},
										new[] {902, 902},
										new[] {904, 906},
										new[] {908, 908},
										new[] {910, 929},
										new[] {931, 974},
										new[] {976, 1013},
										new[] {1015, 1019},
										new[] {1024, 1153},
										new[] {1155, 1158},
										new[] {1162, 1230},
										new[] {1232, 1269},
										new[] {1272, 1273},
										new[] {1280, 1295},
										new[] {1329, 1366},
										new[] {1369, 1369},
										new[] {1377, 1415},
										new[] {1425, 1441},
										new[] {1443, 1465},
										new[] {1467, 1469},
										new[] {1471, 1471},
										new[] {1473, 1474},
										new[] {1476, 1476},
										new[] {1488, 1514},
										new[] {1520, 1522},
										new[] {1536, 1539},
										new[] {1552, 1557},
										new[] {1569, 1594},
										new[] {1600, 1624},
										new[] {1632, 1641},
										new[] {1646, 1747},
										new[] {1749, 1757},
										new[] {1759, 1768},
										new[] {1770, 1788},
										new[] {1791, 1791},
										new[] {1807, 1866},
										new[] {1869, 1871},
										new[] {1920, 1969},
										new[] {2305, 2361},
										new[] {2364, 2381},
										new[] {2384, 2388},
										new[] {2392, 2403},
										new[] {2406, 2415},
										new[] {2433, 2435},
										new[] {2437, 2444},
										new[] {2447, 2448},
										new[] {2451, 2472},
										new[] {2474, 2480},
										new[] {2482, 2482},
										new[] {2486, 2489},
										new[] {2492, 2500},
										new[] {2503, 2504},
										new[] {2507, 2509},
										new[] {2519, 2519},
										new[] {2524, 2525},
										new[] {2527, 2531},
										new[] {2534, 2547},
										new[] {2561, 2563},
										new[] {2565, 2570},
										new[] {2575, 2576},
										new[] {2579, 2600},
										new[] {2602, 2608},
										new[] {2610, 2611},
										new[] {2613, 2614},
										new[] {2616, 2617},
										new[] {2620, 2620},
										new[] {2622, 2626},
										new[] {2631, 2632},
										new[] {2635, 2637},
										new[] {2649, 2652},
										new[] {2654, 2654},
										new[] {2662, 2676},
										new[] {2689, 2691},
										new[] {2693, 2701},
										new[] {2703, 2705},
										new[] {2707, 2728},
										new[] {2730, 2736},
										new[] {2738, 2739},
										new[] {2741, 2745},
										new[] {2748, 2757},
										new[] {2759, 2761},
										new[] {2763, 2765},
										new[] {2768, 2768},
										new[] {2784, 2787},
										new[] {2790, 2799},
										new[] {2801, 2801},
										new[] {2817, 2819},
										new[] {2821, 2828},
										new[] {2831, 2832},
										new[] {2835, 2856},
										new[] {2858, 2864},
										new[] {2866, 2867},
										new[] {2869, 2873},
										new[] {2876, 2883},
										new[] {2887, 2888},
										new[] {2891, 2893},
										new[] {2902, 2903},
										new[] {2908, 2909},
										new[] {2911, 2913},
										new[] {2918, 2927},
										new[] {2929, 2929},
										new[] {2946, 2947},
										new[] {2949, 2954},
										new[] {2958, 2960},
										new[] {2962, 2965},
										new[] {2969, 2970},
										new[] {2972, 2972},
										new[] {2974, 2975},
										new[] {2979, 2980},
										new[] {2984, 2986},
										new[] {2990, 2997},
										new[] {2999, 3001},
										new[] {3006, 3010},
										new[] {3014, 3016},
										new[] {3018, 3021},
										new[] {3031, 3031},
										new[] {3047, 3055},
										new[] {3065, 3065},
										new[] {3073, 3075},
										new[] {3077, 3084},
										new[] {3086, 3088},
										new[] {3090, 3112},
										new[] {3114, 3123},
										new[] {3125, 3129},
										new[] {3134, 3140},
										new[] {3142, 3144},
										new[] {3146, 3149},
										new[] {3157, 3158},
										new[] {3168, 3169},
										new[] {3174, 3183},
										new[] {3202, 3203},
										new[] {3205, 3212},
										new[] {3214, 3216},
										new[] {3218, 3240},
										new[] {3242, 3251},
										new[] {3253, 3257},
										new[] {3260, 3268},
										new[] {3270, 3272},
										new[] {3274, 3277},
										new[] {3285, 3286},
										new[] {3294, 3294},
										new[] {3296, 3297},
										new[] {3302, 3311},
										new[] {3330, 3331},
										new[] {3333, 3340},
										new[] {3342, 3344},
										new[] {3346, 3368},
										new[] {3370, 3385},
										new[] {3390, 3395},
										new[] {3398, 3400},
										new[] {3402, 3405},
										new[] {3415, 3415},
										new[] {3424, 3425},
										new[] {3430, 3439},
										new[] {3458, 3459},
										new[] {3461, 3478},
										new[] {3482, 3505},
										new[] {3507, 3515},
										new[] {3517, 3517},
										new[] {3520, 3526},
										new[] {3530, 3530},
										new[] {3535, 3540},
										new[] {3542, 3542},
										new[] {3544, 3551},
										new[] {3570, 3571},
										new[] {3585, 3642},
										new[] {3647, 3662},
										new[] {3664, 3673},
										new[] {3713, 3714},
										new[] {3716, 3716},
										new[] {3719, 3720},
										new[] {3722, 3722},
										new[] {3725, 3725},
										new[] {3732, 3735},
										new[] {3737, 3743},
										new[] {3745, 3747},
										new[] {3749, 3749},
										new[] {3751, 3751},
										new[] {3754, 3755},
										new[] {3757, 3769},
										new[] {3771, 3773},
										new[] {3776, 3780},
										new[] {3782, 3782},
										new[] {3784, 3789},
										new[] {3792, 3801},
										new[] {3804, 3805},
										new[] {3840, 3840},
										new[] {3864, 3865},
										new[] {3872, 3881},
										new[] {3893, 3893},
										new[] {3895, 3895},
										new[] {3897, 3897},
										new[] {3902, 3911},
										new[] {3913, 3946},
										new[] {3953, 3972},
										new[] {3974, 3979},
										new[] {3984, 3991},
										new[] {3993, 4028},
										new[] {4038, 4038},
										new[] {4096, 4129},
										new[] {4131, 4135},
										new[] {4137, 4138},
										new[] {4140, 4146},
										new[] {4150, 4153},
										new[] {4160, 4169},
										new[] {4176, 4185},
										new[] {4256, 4293},
										new[] {4304, 4344},
										new[] {4352, 4441},
										new[] {4447, 4514},
										new[] {4520, 4601},
										new[] {4608, 4614},
										new[] {4616, 4678},
										new[] {4680, 4680},
										new[] {4682, 4685},
										new[] {4688, 4694},
										new[] {4696, 4696},
										new[] {4698, 4701},
										new[] {4704, 4742},
										new[] {4744, 4744},
										new[] {4746, 4749},
										new[] {4752, 4782},
										new[] {4784, 4784},
										new[] {4786, 4789},
										new[] {4792, 4798},
										new[] {4800, 4800},
										new[] {4802, 4805},
										new[] {4808, 4814},
										new[] {4816, 4822},
										new[] {4824, 4846},
										new[] {4848, 4878},
										new[] {4880, 4880},
										new[] {4882, 4885},
										new[] {4888, 4894},
										new[] {4896, 4934},
										new[] {4936, 4954},
										new[] {4969, 4977},
										new[] {5024, 5108},
										new[] {5121, 5740},
										new[] {5743, 5750},
										new[] {5761, 5786},
										new[] {5792, 5866},
										new[] {5870, 5872},
										new[] {5888, 5900},
										new[] {5902, 5908},
										new[] {5920, 5940},
										new[] {5952, 5971},
										new[] {5984, 5996},
										new[] {5998, 6000},
										new[] {6002, 6003},
										new[] {6016, 6099},
										new[] {6103, 6103},
										new[] {6107, 6109},
										new[] {6112, 6121},
										new[] {6155, 6157},
										new[] {6160, 6169},
										new[] {6176, 6263},
										new[] {6272, 6313},
										new[] {6400, 6428},
										new[] {6432, 6443},
										new[] {6448, 6459},
										new[] {6470, 6509},
										new[] {6512, 6516},
										new[] {7424, 7531},
										new[] {7680, 7835},
										new[] {7840, 7929},
										new[] {7936, 7957},
										new[] {7960, 7965},
										new[] {7968, 8005},
										new[] {8008, 8013},
										new[] {8016, 8023},
										new[] {8025, 8025},
										new[] {8027, 8027},
										new[] {8029, 8029},
										new[] {8031, 8061},
										new[] {8064, 8116},
										new[] {8118, 8124},
										new[] {8126, 8126},
										new[] {8130, 8132},
										new[] {8134, 8140},
										new[] {8144, 8147},
										new[] {8150, 8155},
										new[] {8160, 8172},
										new[] {8178, 8180},
										new[] {8182, 8188},
										new[] {8204, 8207},
										new[] {8234, 8238},
										new[] {8255, 8256},
										new[] {8276, 8276},
										new[] {8288, 8291},
										new[] {8298, 8303},
										new[] {8305, 8305},
										new[] {8319, 8319},
										new[] {8352, 8369},
										new[] {8400, 8412},
										new[] {8417, 8417},
										new[] {8421, 8426},
										new[] {8450, 8450},
										new[] {8455, 8455},
										new[] {8458, 8467},
										new[] {8469, 8469},
										new[] {8473, 8477},
										new[] {8484, 8484},
										new[] {8486, 8486},
										new[] {8488, 8488},
										new[] {8490, 8493},
										new[] {8495, 8497},
										new[] {8499, 8505},
										new[] {8509, 8511},
										new[] {8517, 8521},
										new[] {8544, 8579},
										new[] {12293, 12295},
										new[] {12321, 12335},
										new[] {12337, 12341},
										new[] {12344, 12348},
										new[] {12353, 12438},
										new[] {12441, 12442},
										new[] {12445, 12447},
										new[] {12449, 12543},
										new[] {12549, 12588},
										new[] {12593, 12686},
										new[] {12704, 12727},
										new[] {12784, 12799},
										new[] {13312, 19893},
										new[] {19968, 40869},
										new[] {40960, 42124},
										new[] {44032, 55203},
										new[] {63744, 64045},
										new[] {64048, 64106},
										new[] {64256, 64262},
										new[] {64275, 64279},
										new[] {64285, 64296},
										new[] {64298, 64310},
										new[] {64312, 64316},
										new[] {64318, 64318},
										new[] {64320, 64321},
										new[] {64323, 64324},
										new[] {64326, 64433},
										new[] {64467, 64829},
										new[] {64848, 64911},
										new[] {64914, 64967},
										new[] {65008, 65020},
										new[] {65024, 65039},
										new[] {65056, 65059},
										new[] {65075, 65076},
										new[] {65101, 65103},
										new[] {65129, 65129},
										new[] {65136, 65140},
										new[] {65142, 65276},
										new[] {65279, 65279},
										new[] {65284, 65284},
										new[] {65296, 65305},
										new[] {65313, 65338},
										new[] {65343, 65343},
										new[] {65345, 65370},
										new[] {65381, 65470},
										new[] {65474, 65479},
										new[] {65482, 65487},
										new[] {65490, 65495},
										new[] {65498, 65500},
										new[] {65504, 65505},
										new[] {65509, 65510},
										new[] {65529, 65531},
#endregion
																};

	}
}
