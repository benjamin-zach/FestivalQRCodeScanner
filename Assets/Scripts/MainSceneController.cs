using BarcodeScanner;
using BarcodeScanner.Scanner;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneController : MonoBehaviour
{
	private IScanner BarcodeScanner;
	public Image backgroundImage;
	public Text lastScannedCode;
	public Text statusText;
	public RawImage Image;
	public AudioSource Audio;
	public ScannedCodes scannedCodes;
	public ScrollViewController scrollViewController;
	public Button optionsButton;
	public GameObject optionsPanel;
	public Button removeCodeButton;
	public InputField removeCodeInputField;
	public Button resetButton;
	public DialogPanelController dialogPanel;
	private float RestartTime;
	public static List<string> expectedCodes = new List<string>()
	{
		"Saturnalia2019_2GPHWFUP2",		"Saturnalia2019_0L0DSIV0F",		"Saturnalia2019_DHWKNNPQX",		"Saturnalia2019_UU6B1WLE0",		"Saturnalia2019_742VMQ75W",		"Saturnalia2019_VWBO5F91N",		"Saturnalia2019_VIIR3ZTY0",		"Saturnalia2019_59KSB47BB",
		"Saturnalia2019_M8LR67G0P",		"Saturnalia2019_20MUNN4JX",		"Saturnalia2019_XLMGGW210",		"Saturnalia2019_II0VPOXK0",		"Saturnalia2019_NTD16EG4O",		"Saturnalia2019_IW0CC27TF",		"Saturnalia2019_QY4N4T8FM",		"Saturnalia2019_852XH137Q",
		"Saturnalia2019_JEN6RUVA8",		"Saturnalia2019_BXVFLDQ3B",		"Saturnalia2019_K0H6BMFQP",		"Saturnalia2019_XYG9GHFCW",		"Saturnalia2019_9PO7POYFB",		"Saturnalia2019_T2GYHPSYG",		"Saturnalia2019_LIM2ZP3EK",		"Saturnalia2019_9I7ODCPLY",
		"Saturnalia2019_K499PU0C9",		"Saturnalia2019_EWRM6B749",		"Saturnalia2019_8KOLIDTRM",		"Saturnalia2019_ZCRUSDNEM",		"Saturnalia2019_37MBKTRPR",		"Saturnalia2019_M4Z2DV03I",		"Saturnalia2019_OP3NOT6PT",		"Saturnalia2019_6YQR94USB",
		"Saturnalia2019_P0AYCIU4H",		"Saturnalia2019_PJWJLMBJY",		"Saturnalia2019_RIGCIN1OQ",		"Saturnalia2019_X08PNJPWH",		"Saturnalia2019_GXAQVGZ7A",		"Saturnalia2019_JFTO7MQ26",		"Saturnalia2019_T82GJA8DD",		"Saturnalia2019_QGDWWRTN3",
		"Saturnalia2019_YIOHRBDPU",		"Saturnalia2019_QRCQQGB86",		"Saturnalia2019_4K7RMMM8H",		"Saturnalia2019_S8EGKGOCH",		"Saturnalia2019_WD8CEH4VT",		"Saturnalia2019_U8T00KOJF",		"Saturnalia2019_K45GA4NEA",		"Saturnalia2019_UKU49WRM7",
		"Saturnalia2019_JNZY2XKZ3",		"Saturnalia2019_KWKOE0IVY",		"Saturnalia2019_FQWK68J1Y",		"Saturnalia2019_RZ25GT0JF",		"Saturnalia2019_SU9WLZ3RA",		"Saturnalia2019_2DMEC3PF4",		"Saturnalia2019_B5KK640GX",		"Saturnalia2019_XALLDXOLQ",
		"Saturnalia2019_WFZWO507U",		"Saturnalia2019_ZCZLEY1NY",		"Saturnalia2019_KWWUX66TR",		"Saturnalia2019_RUG4PHRB8",		"Saturnalia2019_0SA3QMDV7",		"Saturnalia2019_68ML210GZ",		"Saturnalia2019_044BC4SKN",		"Saturnalia2019_DLAJWTHUU",
		"Saturnalia2019_BYG0KJX0S",		"Saturnalia2019_M96M40AHG",		"Saturnalia2019_IABKDJD1T",		"Saturnalia2019_2VIBIPYMD",		"Saturnalia2019_DDNIUFX8X",		"Saturnalia2019_P9UGD3JV0",		"Saturnalia2019_XTECB9QKF",		"Saturnalia2019_U1GYZK12O",
		"Saturnalia2019_X8E4WQE4U",		"Saturnalia2019_7VI4O2ZIZ",		"Saturnalia2019_7PEZNLMV6",		"Saturnalia2019_E5AWFBER6",		"Saturnalia2019_FLPCADAXE",		"Saturnalia2019_51K9TSA7G",		"Saturnalia2019_QP8TWSQBT",		"Saturnalia2019_45D5WNW5C",
		"Saturnalia2019_K064B1Y3W",		"Saturnalia2019_N3G4HPZ72",		"Saturnalia2019_F9GACMZNN",		"Saturnalia2019_8XUE3AXXC",		"Saturnalia2019_9VZHFZHHF",		"Saturnalia2019_PRV8VQ77F",		"Saturnalia2019_O4ZQVK854",		"Saturnalia2019_SHDR2I9D7",
		"Saturnalia2019_5H2QVF863",		"Saturnalia2019_7PML1YU5C",		"Saturnalia2019_TCNVQJES7",		"Saturnalia2019_DR70QNNZ1",		"Saturnalia2019_3NDO04327",		"Saturnalia2019_UPG5J2G5A",		"Saturnalia2019_OSQ5TWO78",		"Saturnalia2019_F3T2ZMVRO",
		"Saturnalia2019_4RZXSN4X2",		"Saturnalia2019_TZ52ML189",		"Saturnalia2019_H6M6NRTXP",		"Saturnalia2019_0OJM8LTB1",		"Saturnalia2019_R9G18MY5Q",		"Saturnalia2019_FN6JAEOCG",		"Saturnalia2019_EYMWFLCIQ",		"Saturnalia2019_B4W849X0D",
		"Saturnalia2019_VIS0HME7U",		"Saturnalia2019_CR8UADCNH",		"Saturnalia2019_617V4I48Z",		"Saturnalia2019_BQI4NEKKH",		"Saturnalia2019_YW5LE92LH",		"Saturnalia2019_MFRPSDOYS",		"Saturnalia2019_D8RFGSQTQ",		"Saturnalia2019_I48U9WL22",
		"Saturnalia2019_2LKZFXGKG",		"Saturnalia2019_N0M4O91DZ",		"Saturnalia2019_THI4WLVOT",		"Saturnalia2019_19CSA0PT1",		"Saturnalia2019_VPCUOH3LX",		"Saturnalia2019_YQI31UWHJ",		"Saturnalia2019_SRVHF5TDX",		"Saturnalia2019_1EO8NSHW1",
		"Saturnalia2019_7DJ3CK8RX",		"Saturnalia2019_CUMFDYSE8",		"Saturnalia2019_IJE0FUS3N",		"Saturnalia2019_YXETQ2DMR",		"Saturnalia2019_0K15OZTKS",		"Saturnalia2019_UR759BZIZ",		"Saturnalia2019_83YX0LJNV",		"Saturnalia2019_RODODJCEN",
		"Saturnalia2019_CFVXPT1VH",		"Saturnalia2019_85O04R6HZ",		"Saturnalia2019_V5NRLRABK",		"Saturnalia2019_W5XIN9BUL",		"Saturnalia2019_YIZ0MVQBI",		"Saturnalia2019_BKYY59OHX",		"Saturnalia2019_RR6RHUYT7",		"Saturnalia2019_ICGUG9HML",
		"Saturnalia2019_E1XMNLJTG",		"Saturnalia2019_KBJAHJN6C",		"Saturnalia2019_UZVR4H068",		"Saturnalia2019_Q7KTLKP0A",		"Saturnalia2019_VFA14K5G8",		"Saturnalia2019_K799IOY46",		"Saturnalia2019_NAB36QV2M",		"Saturnalia2019_GX07YPN7T",
		"Saturnalia2019_X50CWSVIA",		"Saturnalia2019_7W7ENHUC8",		"Saturnalia2019_R0JIXGEE3",		"Saturnalia2019_AHMHLR4SH",		"Saturnalia2019_MQZQNXW12",		"Saturnalia2019_GJ1ECUYDU",		"Saturnalia2019_88V0D1AMS",		"Saturnalia2019_FG9FAV2R7",
		"Saturnalia2019_8UNWCKC9Q",		"Saturnalia2019_9X4Q52UGH",		"Saturnalia2019_88QRPQEQ4",		"Saturnalia2019_GY4IKHGEU",		"Saturnalia2019_GI7O6I1AT",		"Saturnalia2019_C9O6IOYFV",		"Saturnalia2019_QC33U84EL",		"Saturnalia2019_92K4B4HLJ",
		"Saturnalia2019_1PQ3H3MPU",		"Saturnalia2019_GIQUQD1W0",		"Saturnalia2019_J632SXBLM",		"Saturnalia2019_26J0S700A",		"Saturnalia2019_Z7GAKR2S6",		"Saturnalia2019_B0YGP0XJB",		"Saturnalia2019_7Q5FLGY9X",		"Saturnalia2019_SUEPX5JVK",
		"Saturnalia2019_1EAGNUR75",		"Saturnalia2019_DTADGAL3T",		"Saturnalia2019_SBI0D69DD",		"Saturnalia2019_CXRMVRAX7",		"Saturnalia2019_M62BRZFS9",		"Saturnalia2019_7UL6ICA6F",		"Saturnalia2019_HWK6307H6",		"Saturnalia2019_MU295IOHT",
		"Saturnalia2019_VRMPMUK7A",		"Saturnalia2019_OAAF942RV",		"Saturnalia2019_97Y2X0MO9",		"Saturnalia2019_UWT990PQE",		"Saturnalia2019_D2BMYMOA5",		"Saturnalia2019_01UCISCX7",		"Saturnalia2019_9S8ECJS2B",		"Saturnalia2019_QSX4EN5ZR",
		"Saturnalia2019_K9DXNMPYN",		"Saturnalia2019_V9UCWJG04",		"Saturnalia2019_Q3R6D7BDR",		"Saturnalia2019_09N2LRY4P",		"Saturnalia2019_KTMADKU22",		"Saturnalia2019_9I5P6YM39",		"Saturnalia2019_90D68LOGR",		"Saturnalia2019_GAZZIAIJC",
		"Saturnalia2019_VYQIRH3A6",		"Saturnalia2019_6MBACOKUM",		"Saturnalia2019_M5XQWW46P",		"Saturnalia2019_4RF76VSQD",		"Saturnalia2019_5J23JEXWB",		"Saturnalia2019_9G034WEL6",		"Saturnalia2019_B4NI3THUF",		"Saturnalia2019_MPRL98GBF",
		"Saturnalia2019_E0WL5S9LS",		"Saturnalia2019_PP8VU6LHK",		"Saturnalia2019_8TZ1CFIBJ",		"Saturnalia2019_LAIGH1QRG",		"Saturnalia2019_9QNIR9CQD",		"Saturnalia2019_D6Y3D7H9F",		"Saturnalia2019_BLPY19MMG",		"Saturnalia2019_YDPXNUTRK",
		"Saturnalia2019_5R84B6NXQ",		"Saturnalia2019_QCNFOOCIF",		"Saturnalia2019_X1B7H4THC",		"Saturnalia2019_2AJTON9W2",		"Saturnalia2019_13ADJHYBP",		"Saturnalia2019_FKTGGFT0C",		"Saturnalia2019_1NJ8I4MU4",		"Saturnalia2019_PQWGN1KXK",
		"Saturnalia2019_PPANB1AJ8",		"Saturnalia2019_2UZAK32LY",		"Saturnalia2019_4ONAHZJJ1",		"Saturnalia2019_X0THQM5L9",		"Saturnalia2019_XOO7W5VKK",		"Saturnalia2019_67VJ5J9FM",		"Saturnalia2019_AA6SUAMIK",		"Saturnalia2019_I26YIGXX0",
		"Saturnalia2019_JRKQE8WYS",		"Saturnalia2019_IMB4DIFBD",		"Saturnalia2019_QEVGSQG6P",		"Saturnalia2019_9EN6A27N3",		"Saturnalia2019_6A4AX8W6J",		"Saturnalia2019_DCC0LC19F",		"Saturnalia2019_THWRFRVR4",		"Saturnalia2019_JBJPIC4E8",
		"Saturnalia2019_4SOXJS2G1",		"Saturnalia2019_ZWX3IXKB8",		"Saturnalia2019_XBC2P6P66",		"Saturnalia2019_66O8FVNF9",		"Saturnalia2019_MQTEKISFV",		"Saturnalia2019_4MR1C2OOX",		"Saturnalia2019_CRNMVZ3NJ",		"Saturnalia2019_7PJ3HKOU7",
		"Saturnalia2019_CKF5WHP8X",		"Saturnalia2019_GKL3H17NH",		"Saturnalia2019_YGEDZR07R",		"Saturnalia2019_BHTKQOSC7",		"Saturnalia2019_7U0I60RHI",		"Saturnalia2019_TT0KRHVZU",		"Saturnalia2019_5IKPBARKP",		"Saturnalia2019_KIY8GIF42",
		"Saturnalia2019_97KY7NT16",		"Saturnalia2019_CFRGF7AC4",		"Saturnalia2019_MQ80IPEPT",		"Saturnalia2019_BT8GVW7FM",		"Saturnalia2019_W9VTQRT80",		"Saturnalia2019_ZT3M96F6U",		"Saturnalia2019_LW6CLA2QW",		"Saturnalia2019_HN8KTLOSG",
		"Saturnalia2019_BDUINZY9G",		"Saturnalia2019_378KJK6IZ",		"Saturnalia2019_OR3N28MTO",		"Saturnalia2019_ZGGJZIJBP",		"Saturnalia2019_HYDFU1UG7",		"Saturnalia2019_19N9WSNU9",		"Saturnalia2019_MB3XBTNXC",		"Saturnalia2019_6Z7JM93PD",
		"Saturnalia2019_S2Q50CUY7",		"Saturnalia2019_O6JOF3A5X",		"Saturnalia2019_D17A7CPOU",		"Saturnalia2019_CMT3MZ023",		"Saturnalia2019_HK1B2X4QT",		"Saturnalia2019_NCPQI87XD",		"Saturnalia2019_Z6H68LXFD",		"Saturnalia2019_M8TJB7ZUI",
		"Saturnalia2019_7ANAS65JU",		"Saturnalia2019_0RTI1NEP6",		"Saturnalia2019_JIY7AEUZH",		"Saturnalia2019_GGFX5SJET",		"Saturnalia2019_Y0AKC7C4L",		"Saturnalia2019_1NBCYEL11",		"Saturnalia2019_AH1O57A46",		"Saturnalia2019_VB4SJB1L6",
		"Saturnalia2019_YJ0W50QTO",		"Saturnalia2019_DV8SQ2T48",		"Saturnalia2019_50CK0EBZQ",		"Saturnalia2019_EOYJUCLHR",		"Saturnalia2019_GISF9NJ9W",		"Saturnalia2019_VAXSHDAIR",		"Saturnalia2019_BUXCMHO6D",		"Saturnalia2019_H876WA8W8",
		"Saturnalia2019_G1HXCM4VI",		"Saturnalia2019_JUOKGEU5I",		"Saturnalia2019_3CZ7SRM41",		"Saturnalia2019_B2YZQTZI5",		"Saturnalia2019_AH4HYAJ8W",		"Saturnalia2019_AMD4VVTB6",		"Saturnalia2019_YG57XODZB",		"Saturnalia2019_QTJU0F3BN",
		"Saturnalia2019_N39V0PDUH",		"Saturnalia2019_EBFJ44OI1",		"Saturnalia2019_MVLG4MHK8",		"Saturnalia2019_5WW3KHPTG",		"Saturnalia2019_EZ3HRT5KZ",		"Saturnalia2019_4ZJKLG7SE",		"Saturnalia2019_YASJLXYQR",		"Saturnalia2019_6T88TSV7C",
		"Saturnalia2019_YWA4KLCBQ",		"Saturnalia2019_OWWKJTR3P",		"Saturnalia2019_0TL5Y48XE",		"Saturnalia2019_2VAV1EQEG",		"Saturnalia2019_4BTAQ0PRS",		"Saturnalia2019_T3SFOOWO7",		"Saturnalia2019_33LHCLO6B",		"Saturnalia2019_IKZJY3KLT",
		"Saturnalia2019_KCR1OWSOM",		"Saturnalia2019_BQXA0GTQ3",		"Saturnalia2019_XKF86LJ6D",		"Saturnalia2019_Z6MDP6RXS",		"Saturnalia2019_H168LQUPV",		"Saturnalia2019_RL39KR9SP",		"Saturnalia2019_GOJLDWY0P",		"Saturnalia2019_8KDVAV890",
		"Saturnalia2019_UYTZ8ON08",		"Saturnalia2019_YF33C5723",		"Saturnalia2019_56TWJ6WWN",		"Saturnalia2019_4VI6NMASM",		"Saturnalia2019_3QF0GP1A6",		"Saturnalia2019_J7GUC6GHL",		"Saturnalia2019_AARYGU4ZW",		"Saturnalia2019_S9WQ2BSAZ",
		"Saturnalia2019_QP84WHT96",		"Saturnalia2019_988LJMY6T",		"Saturnalia2019_BAW2JAYJY",		"Saturnalia2019_IU43K77EJ",		"Saturnalia2019_YEXMRIODJ",		"Saturnalia2019_R6HS05YRN",		"Saturnalia2019_NH49P677U",		"Saturnalia2019_FDYOIXT4E",
		"Saturnalia2019_KTKWGJU1G",		"Saturnalia2019_KESIZQKCL",		"Saturnalia2019_CXGAJW71A",		"Saturnalia2019_R0SLYTR66",		"Saturnalia2019_JGRMKN2KJ",		"Saturnalia2019_X9L5TK5CB",		"Saturnalia2019_HMPQ539BK",		"Saturnalia2019_75841RYD8",
		"Saturnalia2019_7N9AHYRBO",		"Saturnalia2019_GQ5I1NTOU",		"Saturnalia2019_ZA84SM2BS",		"Saturnalia2019_Y51Y83450",		"Saturnalia2019_XX8AK4CSM",		"Saturnalia2019_HTSTR43MM",		"Saturnalia2019_3UEMWB0WA",		"Saturnalia2019_AFDEHN62O",
		"Saturnalia2019_19BB83U0O",		"Saturnalia2019_FMOEJIISM",		"Saturnalia2019_AT8BLVSZ5",		"Saturnalia2019_94R8AQNOT",		"Saturnalia2019_Q5LAQ95OF",		"Saturnalia2019_XYJMLDILD",		"Saturnalia2019_RNW3NK355",		"Saturnalia2019_167PZ94GR",
		"Saturnalia2019_06LS2WKQW",		"Saturnalia2019_DZKYV1F7C",		"Saturnalia2019_UHCLGZMCA",		"Saturnalia2019_SNY0VCK3E",		"Saturnalia2019_UFJWNIY4T",		"Saturnalia2019_TU4JG78J8",		"Saturnalia2019_V4SVLODN5",		"Saturnalia2019_02PGCWUW8",
		"Saturnalia2019_9WNMSUBHA",		"Saturnalia2019_7O89HVP7B",		"Saturnalia2019_T1CO4HK00",		"Saturnalia2019_XA0WAS46R",		"Saturnalia2019_W1V73W1FS",		"Saturnalia2019_92QDAQGAH",		"Saturnalia2019_UKVWEOU4F",		"Saturnalia2019_1A6R7TLBI",
		"Saturnalia2019_FEI29NLXQ",		"Saturnalia2019_61M8GF3OW",		"Saturnalia2019_38V5BY65O",		"Saturnalia2019_IYZXLCF9I",		"Saturnalia2019_IVSDWY4N8",		"Saturnalia2019_N2HJ3S8TE",		"Saturnalia2019_2L07MDD3C",		"Saturnalia2019_GWHO1M2QM",
		"Saturnalia2019_KR4P4NP44",		"Saturnalia2019_GZA4814UC",		"Saturnalia2019_B10XD1FQ4",		"Saturnalia2019_NHGAPT3RQ",		"Saturnalia2019_USYLNAPW1",		"Saturnalia2019_IUDMLQD25",		"Saturnalia2019_NBH9X9R1B",		"Saturnalia2019_5IEGS3SRY",
		"Saturnalia2019_EJJI0JU1M",		"Saturnalia2019_ATRD08SEU",		"Saturnalia2019_Z7P4PIAT8",		"Saturnalia2019_985J50OP7",		"Saturnalia2019_TF8744GOG",		"Saturnalia2019_KTD9HTOC1",		"Saturnalia2019_LE4NP7DKX",		"Saturnalia2019_DPEWDGYHV",
		"Saturnalia2019_Q3YRMCQ99",		"Saturnalia2019_FASSKPZDB",		"Saturnalia2019_YL813XP94",		"Saturnalia2019_XA3CWGZD0",		"Saturnalia2019_OPHUSPZA3",		"Saturnalia2019_4O7HDFYNK",		"Saturnalia2019_19CEZOPIB",		"Saturnalia2019_3VNKDYPDW",
		"Saturnalia2019_PBZCY99P5",		"Saturnalia2019_650RVC4TP",		"Saturnalia2019_96HBD5XJ8",		"Saturnalia2019_TIBDUIE74",		"Saturnalia2019_PUUK0CUH7",		"Saturnalia2019_2P2RMHULA",		"Saturnalia2019_505NA44SC",		"Saturnalia2019_JDPYLTXWC",
		"Saturnalia2019_9SHUGNJTI",		"Saturnalia2019_S69PG1UA8",		"Saturnalia2019_E75YLHYTF",		"Saturnalia2019_8QD53OP42",		"Saturnalia2019_V0CGGR9J1",		"Saturnalia2019_QKB6XJ18R",		"Saturnalia2019_203GG0VHT",		"Saturnalia2019_73ELP7N1S",
		"Saturnalia2019_8RMZYH2N9",		"Saturnalia2019_LKMI6FTZQ",		"Saturnalia2019_Y1WW6B0C2",		"Saturnalia2019_JNF50JNR6",		"Saturnalia2019_VHZODTJID",		"Saturnalia2019_7356DN4WX",		"Saturnalia2019_IF2B55UQB",		"Saturnalia2019_6XH3DHHWF",
		"Saturnalia2019_KC40EYMCQ",		"Saturnalia2019_8Q99LO71K",		"Saturnalia2019_K3M0G9RAB",		"Saturnalia2019_4WKLW3ABC",		"Saturnalia2019_XVID9DKIV",		"Saturnalia2019_KQSKBMQHJ",		"Saturnalia2019_A3WSENW2T",		"Saturnalia2019_SARIE3NE2",
		"Saturnalia2019_WBMI5L87O",		"Saturnalia2019_WWEN4I51I",		"Saturnalia2019_WMJ4DUHHM",		"Saturnalia2019_SN6G2OCPU",		"Saturnalia2019_H12025KPV",		"Saturnalia2019_2N028T2U8",		"Saturnalia2019_SVGHV4917",		"Saturnalia2019_UX59I4S09",
		"Saturnalia2019_K2TFJJ09F",		"Saturnalia2019_P7JTU47UF",		"Saturnalia2019_N8UTSAK4F",		"Saturnalia2019_K33L2QZ36",		"Saturnalia2019_7CGKV93SG",		"Saturnalia2019_OU2UK9AQ2",		"Saturnalia2019_ZYS32ERSF",		"Saturnalia2019_IWT9R21EX",
		"Saturnalia2019_7W93KC6NH",		"Saturnalia2019_2UBFRLNPL",		"Saturnalia2019_R82L62H4T",		"Saturnalia2019_3MHC0KPA0",		"Saturnalia2019_LDRLE1Z9V",		"Saturnalia2019_S9UB35F6P",		"Saturnalia2019_X5NFZEJ2D",		"Saturnalia2019_EJSEM6UWK",
		"Saturnalia2019_T6GP45Q5S",		"Saturnalia2019_8OV5A0O6S",		"Saturnalia2019_5ILK4Q9O0",		"Saturnalia2019_F92NH6I0Q",		"Saturnalia2019_WIJ4TE0F8",		"Saturnalia2019_E8INAUR0C",		"Saturnalia2019_4GMNM0HSV",		"Saturnalia2019_2M41YBFN4",
		"Saturnalia2019_X7PUV5GRU",		"Saturnalia2019_IH4RU9F1S",		"Saturnalia2019_KEH6X637D",		"Saturnalia2019_PUV47FMXT",		"Saturnalia2019_3DF03K0KW",		"Saturnalia2019_89PJOCOZH",		"Saturnalia2019_JS2HXW5H0",		"Saturnalia2019_87CWXLSP3",
		"Saturnalia2019_C51UMY6OW",		"Saturnalia2019_DEXONINLT",		"Saturnalia2019_NE3CVAAHG",		"Saturnalia2019_9MM4CSW40",		"Saturnalia2019_LJ5AZS5NT",		"Saturnalia2019_FB95K3ES8",		"Saturnalia2019_TRWV0RTLD",		"Saturnalia2019_MPJOW2WMK",
		"Saturnalia2019_NQX35NPRW",		"Saturnalia2019_CSE4U710U",		"Saturnalia2019_C74EZBIRZ",		"Saturnalia2019_4I9RYY2LF",		"Saturnalia2019_K2C3R84QC",		"Saturnalia2019_OL8MO822S",		"Saturnalia2019_86DO7VFN2",		"Saturnalia2019_3IZAKOPY1",
		"Saturnalia2019_6WCOAJXW4",		"Saturnalia2019_BMJP7NKQ8",		"Saturnalia2019_JLNOKJ9LF",		"Saturnalia2019_11T02FRNS"
	};

	// Disable Screen Rotation on that screen
	void Awake()
	{
		statusText.text = "SORTING CODES";
		expectedCodes.Sort();
		statusText.text = "";
		Screen.autorotateToPortrait = false;
		Screen.autorotateToPortraitUpsideDown = false;
		Screen.orientation = ScreenOrientation.Portrait;
	}

	void Start()
	{
		// Create a basic scanner
		BarcodeScanner = new Scanner();
		BarcodeScanner.Camera.Play();

		// Display the camera texture through a RawImage
		BarcodeScanner.OnReady += (sender, arg) => {
			// Set Orientation & Texture
			Image.transform.localEulerAngles = BarcodeScanner.Camera.GetEulerAngles();
			//Image.transform.localScale = BarcodeScanner.Camera.GetScale();
			Image.texture = BarcodeScanner.Camera.Texture;

			// Keep Image Aspect Ratio
			var rect = Image.GetComponent<RectTransform>();
			var newHeight = rect.sizeDelta.x * BarcodeScanner.Camera.Height / BarcodeScanner.Camera.Width;
			rect.sizeDelta = new Vector2(rect.sizeDelta.x, newHeight);

			RestartTime = Time.realtimeSinceStartup;
		};

		optionsButton.onClick.AddListener(OnOptionsButton);
		removeCodeButton.onClick.AddListener(OnRemoveCodeButton);
		resetButton.onClick.AddListener(OnResetButton);

		RefreshView();
	}

	private void RefreshView()
	{
		scrollViewController.Init(scannedCodes.data);
	}

	/// <summary>
	/// Start a scan and wait for the callback (wait 1s after a scan success to avoid scanning multiple time the same element)
	/// </summary>
	private void StartScanner()
	{
		BarcodeScanner.Scan(OnQrCodeRecognized);
	}

	/// <summary>
	/// The Update method from unity need to be propagated
	/// </summary>
	void Update()
	{
		if (BarcodeScanner != null)
		{
			BarcodeScanner.Update();
		}

		// Check if the Scanner need to be started or restarted
		if (RestartTime != 0 && RestartTime < Time.realtimeSinceStartup)
		{
			StartScanner();
			RestartTime = 0;
			statusText.color = Color.white;
			statusText.text = "READY";
			backgroundImage.color = Color.white;
		}
	}

	private void OnQrCodeRecognized(string barCodeType, string barCodeValue)
	{
		BarcodeScanner.Stop();

		// Feedback
		Audio.Play();

#if UNITY_ANDROID || UNITY_IOS
			Handheld.Vibrate();
#endif

		if(scannedCodes.Contains(barCodeValue))
		{
			statusText.color = Color.red;
			statusText.text = "ALREADY SCANNED";
			backgroundImage.color = Color.red;
		}
		else if(!expectedCodes.Contains(barCodeValue))
		{
			statusText.color = Color.yellow;
			statusText.text = "UNKNOWN CODE";
			backgroundImage.color = Color.yellow;
		}
		else
		{
			statusText.color = Color.green;
			statusText.text = "NEW QRCODE ADDED";
			scannedCodes.Add(barCodeValue);
			scannedCodes.Save();
			scrollViewController.Init(scannedCodes.data);
			backgroundImage.color = Color.green;
		}
		RestartTime += Time.realtimeSinceStartup + 2.5f;
		lastScannedCode.text = barCodeValue;
	}

	private void OnOptionsButton()
	{
		optionsPanel.SetActive(!optionsPanel.activeSelf);
	}

	private void OnRemoveCodeButton()
	{
		var code = removeCodeInputField.text;
		scannedCodes.Remove(code);
		scannedCodes.Save();
		RefreshView();
	}

	private void OnResetButton()
	{
		dialogPanel.Show(() =>
		{
			scannedCodes.Reset();
			dialogPanel.Dismiss();
			scannedCodes.Save();
			RefreshView();
		}, () =>
		{
			dialogPanel.Dismiss();
		});
	}
}
