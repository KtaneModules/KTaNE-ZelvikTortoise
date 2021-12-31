using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using System.Text.RegularExpressions;

public class levenshteinDistanceScript : MonoBehaviour
{
    public KMAudio Audio;
    public KMBombInfo Bomb;

    public KMSelectable[] digits;
    public KMSelectable buttonClr;
    public KMSelectable buttonDel;
    public KMSelectable buttonSub;
    public TextMesh[] inputWords;
    public TextMesh answerText;

    private string[] possibleWords = { "ABSENT", "ABSTRACT", "ABYSMAL", "ACCIDENT", "ACTIVATE", "ADJACENT", "AFRAID", "AGENDA", "AGONY", "ALCHEMY", "ALCOHOL", "ALIVE", "ALLERGIC", "ALLERGY", "ALPHA", "ALPHABET", "ALREADY", "AMETHYST", "AMNESTY", "AMPERAGE", "ANCIENT", "ANIMALS", "ANIMATE", "ANTHRAX", "ANXIOUS", "AQUARIUM", "AQUARIUS", "ARCADE", "ARRANGE", "ARROW", "ARTEFACT", "ASTERISK", "ATROPHY", "AUDIO", "AUTHOR", "AVOID", "AWESOME", "BALANCE", "BANANA", "BANDIT", "BANKRUPT", "BASKET", "BATTLE", "BAZAAR", "BEARD", "BEAUTY", "BEAVER", "BECOMING", "BEETLE", "BESEECH", "BETWEEN", "BICYCLE", "BIGGER", "BIGGEST", "BIOLOGY", "BIRTHDAY", "BISTRO", "BITES", "BLIGHT", "BLOCKADE", "BLUBBER", "BOMB", "BONOBO", "BOOKS", "BOTTLE", "BRAZIL", "BRIEF", "BROCCOLI", "BROKEN", "BROTHER", "BUBBLE", "BUDGET", "BULKHEAD", "BUMPER", "BUNNY", "BUTTON", "BYTES", "CABLES", "CALIBER", "CAMPAIGN", "CANADA", "CANISTER", "CAPTION", "CAUTION", "CAVITY", "CHALK", "CHAMBER", "CHAMFER", "CHAMPION", "CHANGES", "CHICKEN", "CHILDREN", "CHLORINE", "CHORD", "CHRONIC", "CHURCH", "CINNAMON", "CIVIC", "CLERIC", "CLOCK", "COCOON", "COMBAT", "COMBINE", "COMEDY", "COMICS", "COMMA", "COMMAND", "COMMENT", "COMPOST", "COMPUTER", "CONDOM", "CONFLICT", "CONSIDER", "CONTOUR", "CONTROL", "CORRUPT", "COSTUME", "CRIMINAL", "CRUNCH", "CRYPTIC", "CUBOID", "CYPHER", "DADDY", "DANCER", "DANCING", "DAUGHTER", "DEAD", "DECAPOD", "DECAY", "DECOY", "DEFEAT", "DEFUSER", "DEGREE", "DELAY", "DEMIGOD", "DENTIST", "DESERT", "DESIGN", "DESIRE", "DESSERT", "DETAIL", "DEVELOP", "DEVICE", "DIAMOND", "DICTATE", "DIFFUSE", "DILEMMA", "DINGY", "DINOSAUR", "DISEASE", "DISGUST", "DOCUMENT", "DOUBLED", "DOUBT", "DOWNBEAT", "DRAGON", "DRAWER", "DREAM", "DRINK", "DRUNKEN", "DUNGEON", "DYNASTY", "DYSLEXIA", "ECLIPSE", "ELDRITCH", "EMAIL", "EMULATOR", "ENCRYPT", "ENGLAND", "ENLIST", "ENOUGH", "ENSURE", "EQUALITY", "EQUATION", "ERUPTION", "ETERNITY", "EUPHORIA", "EXACT", "EXCLAIM", "EXHAUST", "EXPERT", "EXPERTLY", "EXPLAIN", "EXPLODES", "FABRIC", "FACTORY", "FADED", "FAINT", "FAIR", "FALSE", "FALTER", "FAMOUS", "FANTASY", "FARM", "FATHER", "FAUCET", "FAULTY", "FEARSOME", "FEAST", "FEBRUARY", "FEINT", "FESTIVAL", "FICTION", "FIGHTER", "FIGURE", "FINISH", "FIREMAN", "FIREWORK", "FIRST", "FIXTURE", "FLAGRANT", "FLAGSHIP", "FLAMINGO", "FLESH", "FLIPPER", "FLUORINE", "FLUSH", "FOREIGN", "FORENSIC", "FRACTAL", "FRAGRANT", "FRANCE", "FRANTIC", "FREAK", "FRICTION", "FRIDAY", "FRIENDLY", "FRIGHTEN", "FUROR", "FUSED", "GARAGE", "GENES", "GENETIC", "GENIUS", "GENTLE", "GLACIER", "GLITCH", "GOAT", "GOLDEN", "GRANULAR", "GRAPHICS", "GRAPHITE", "GRATEFUL", "GRIDLOCK", "GROUND", "GUITAR", "GUMPTION", "HALOGEN", "HARMONY", "HAWK", "HEADACHE", "HEARD", "HEDGEHOG", "HEINOUS", "HERD", "HERETIC", "HEXAGON", "HICCUP", "HIGHWAY", "HOLIDAY", "HOME", "HOMESICK", "HONEST", "HORROR", "HORSE", "HOUSE", "HUGE", "HUMANITY", "HUNGRY", "HYDROGEN", "HYSTERIA", "IMAGINE", "INDUSTRY", "INFAMOUS", "INSIDE", "INTEGRAL", "INTEREST", "IRONCLAD", "ISSUE", "ITALIC", "ITALY", "ITCH", "JAUNDICE", "JEANS", "JEOPARDY", "JOYFUL", "JOYSTICK", "JUICE", "JUNCTURE", "JUNGLE", "JUNKYARD", "JUSTICE", "KEEP", "KEYBOARD", "KILOBYTE", "KILOGRAM", "KINGDOM", "KITCHEN", "KITTEN", "KNIFE", "KRYPTON", "LADYLIKE", "LANGUAGE", "LARGE", "LAUGHTER", "LAUNCH", "LEADERS", "LEARN", "LEAVE", "LEOPARD", "LEVEL", "LIBERAL", "LIBERTY", "LIFEBOAT", "LIGAMENT", "LIGHT", "LIQUID", "LISTEN", "LITTLE", "LOBSTER", "LOGICAL", "LOVE", "LUCKY", "LULLED", "LUNATIC", "LURKS", "MACHINE", "MADAM", "MAGNETIC", "MANAGER", "MANUAL", "MARINA", "MARINE", "MARTIAN", "MASTER", "MATRIX", "MEASURE", "MEATY", "MEDDLE", "MEDICAL", "MENTAL", "MENU", "MEOW", "MERCHANT", "MESSAGE", "MESSES", "METAL", "METHOD", "METTLE", "MILITANT", "MINIM", "MINIMUM", "MIRACLE", "MIRROR", "MISJUDGE", "MISPLACE", "MISSES", "MISTAKE", "MIXTURE", "MNEMONIC", "MOBILE", "MODERN", "MODEST", "MODULE", "MOIST", "MONEY", "MORNING", "MOST", "MOTHER", "MOVIES", "MULTIPLE", "MUNCH", "MUSICAL", "MUSTACHE", "MYSTERY", "MYSTIC", "MYSTIQUE", "MYTHIC", "NARCOTIC", "NASTY", "NATURE", "NAVIGATE", "NETWORK", "NEUTRAL", "NOBELIUM", "NOBODY", "NOISE", "NOTICE", "NOUN", "NUCLEAR", "NUMERAL", "NUTRIENT", "NYMPH", "OBELISK", "OBSTACLE", "OBVIOUS", "OCTOPUS", "OFFSET", "OMEGA", "OPAQUE", "OPINION", "ORANGE", "ORGANIC", "OUCH", "OUTBREAK", "OUTDO", "OVERCAST", "OVERLAPS", "PACKAGE", "PADLOCK", "PANCAKE", "PANDA", "PANIC", "PAPER", "PAPERS", "PARENT", "PARK", "PARTICLE", "PASSIVE", "PATENTED", "PATHETIC", "PATIENT", "PEACE", "PEASANT", "PENALTY", "PENCIL", "PENGUIN", "PERFECT", "PERSON", "PERSUADE", "PERUSING", "PHONE", "PHYSICAL", "PIANO", "PICTURE", "PIGLET", "PILFER", "PILLAGE", "PINCH", "PIRATE", "PITCHER", "PIZZA", "PLANE", "PLANET", "PLATONIC", "PLAYER", "PLEASE", "PLUCKY", "PLUNDER", "PLURALS", "POCKET", "POLICE", "PORTRAIT", "POTATO", "POTENTLY", "POUNCE", "POVERTY", "PRACTICE", "PREDICT", "PREFECT", "PREMIUM", "PRESENT", "PRINCE", "PRINTER", "PRISON", "PROFIT", "PROMISE", "PROPHET", "PROTEIN", "PROVINCE", "PSALM", "PSYCHIC", "PUDDLE", "PUNCHBAG", "PUNGENT", "PUNISH", "PURCHASE", "QUAGMIRE", "QUALIFY", "QUANTIFY", "QUANTIZE", "QUARTER", "QUERYING", "QUEUE", "QUICHE", "QUICK", "RABBIT", "RACOON", "RADAR", "RADICAL", "RAINBOW", "RANDOM", "RATTLE", "RAVENOUS", "REASON", "REBUKE", "REFINE", "REGULAR", "REINDEER", "REQUEST", "RESORT", "RESPECT", "RETIRE", "REVOLT", "REWARD", "RHAPSODY", "RHENIUM", "RHODIUM", "RHOMBOID", "RHYME", "RHYTHM", "RIDICULE", "ROADWORK", "ROAR", "ROAST", "ROOM", "ROOSTER", "ROSTER", "ROTOR", "ROTUNDA", "ROYAL", "RULER", "RURAL", "SAILOR", "SAINTED", "SALES", "SALLY", "SATISFY", "SAUNTER", "SCALE", "SCANDAL", "SCHEDULE", "SCHOOL", "SCIENCE", "SCRATCH", "SCREEN", "SENSIBLE", "SEPARATE", "SERIOUS", "SEVERAL", "SHAMPOO", "SHARES", "SHELTER", "SHIFT", "SHIP", "SHIRT", "SHIVER", "SHORTEN", "SHOWCASE", "SHUFFLE", "SILENT", "SIMILAR", "SISTER", "SIXTH", "SIXTY", "SKATER", "SKYWARD", "SLANDER", "SLAYER", "SLEEK", "SLIPPER", "SMART", "SMEARED", "SOCCER", "SOCIETY", "SOURCE", "SPAIN", "SPARE", "SPARK", "SPATULA", "SPEAKER", "SPECIAL", "SPECTATE", "SPECTRUM", "SPICY", "SPINACH", "SPIRAL", "SPLENDID", "SPLINTER", "SPRAYED", "SPREAD", "SPRING", "SQUADRON", "SQUANDER", "SQUASH", "SQUIB", "SQUID", "SQUISH", "STAKE", "STALKING", "STEAK", "STEAM", "STICKER", "STINKY", "STOCKING", "STONE", "STORE", "STORMY", "STRANGE", "STRIKE", "STUTTER", "SUBWAY", "SUFFER", "SUPREME", "SURF", "SURPLUS", "SURVEY", "SWITCH", "SYMBOL", "SYSTEM", "SYSTEMIC", "TABLE", "TADPOLE", "TALKING", "TANGLE", "TANK", "TAPEWORM", "TARGET", "TAROT", "TEACH", "TEAMWORK", "TERMINAL", "TERMINUS", "TERROR", "TESTIFY", "THEIR", "THERE", "THICK", "THIEF", "THINK", "THROAT", "THROUGH", "THUNDER", "THYME", "TICKET", "TIME", "TOASTER", "TOMATO", "TONE", "TORQUE", "TORTOISE", "TOUCHY", "TOUPE", "TOWER", "TRANSFIX", "TRANSIT", "TRASH", "TRAUMA", "TREASON", "TREASURE", "TRICK", "TRIPOD", "TROUBLE", "TRUCK", "TRUMPET", "TURTLE", "TWINKLE", "UGLY", "ULTRA", "UMBRELLA", "UNDERWAY", "UNIQUE", "UNKNOWN", "UNSTEADY", "UNTOWARD", "UNWASHED", "UPGRADE", "URBAN", "USED", "USELESS", "UTOPIA", "VACUUM", "VAMPIRE", "VANISH", "VANQUISH", "VARIOUS", "VAST", "VELOCITY", "VENDOR", "VERB", "VERBATIM", "VERDICT", "VEXATION", "VICIOUS", "VICTIM", "VICTORY", "VIDEO", "VIEW", "VIKING", "VILLAGE", "VIOLENT", "VIOLIN", "VIRULENT", "VISCERAL", "VISION", "VOLATILE", "VOLTAGE", "VORTEX", "VULGAR", "WARDEN", "WARLOCK", "WARNING", "WEALTH", "WEAPON", "WEDDING", "WEIGHT", "WHACK", "WHARF", "WHAT", "WHEN", "WHISK", "WHISTLE", "WICKED", "WINDOW", "WINTER", "WITNESS", "WIZARD", "WRENCH", "WRETCH", "WRINKLE", "WRITER", "XANTHOUS", "YACHT", "YARN", "YAWN", "YEAH", "YEARLONG", "YEARN", "YEOMAN", "YODEL", "YOGA", "YONDER", "YOUNGEST", "YOURSELF", "ZEALOT", "ZEBRA", "ZENITH", "ZITHER", "ZODIAC", "ZOMBIE" };    // Word lengths: 4–8
    private int[] chosenWordsIndeces = new int[2];
    private bool wordsPicked = false;
    private int correctAnswer;

    // Configuration:
    private const int maxDigits = 2;

    // Logging:
    private static int moduleIdCounter = 1;
    private int moduleId;
    private bool moduleSolved = false;
    private int levDistance;
    private bool LDinSerial = false;
    private const char space = ' ';
    private const char separatorLeftRight = '|';
    private const char separatorTopBottom = '-';
    private const char separatorBoth = '+';
    private const char newLine = '\n';

    void Awake()
    {
        moduleId = moduleIdCounter++;

        foreach (KMSelectable digit in digits)
        {
            KMSelectable pressedDigit = digit;
            digit.OnInteract += delegate () { DigitButtonPressed(pressedDigit); return false; };
        }

        buttonClr.OnInteract += delegate () { PressButtonClr(); return false; };
        buttonDel.OnInteract += delegate () { PressButtonDel(); return false; };
        buttonSub.OnInteract += delegate () { PressButtonSub(); return false; };
    }
    void Start()
    {
        if (!wordsPicked)
        {
            PickWords();
            wordsPicked = true;
        }

        correctAnswer = DetermineCorrectAnswer(out levDistance);
        Debug.LogFormat("[Levenshtein Distance #{0}] You are calculating the Levenshtein distance of words {1} and {2}, which is {3}.", moduleId, possibleWords[chosenWordsIndeces[0]], possibleWords[chosenWordsIndeces[1]], levDistance.ToString());
        if (LDinSerial)
            Debug.LogFormat("[Levenshtein Distance #{0}] There is 'L' and 'D' in the Serial Number. No modifications take place. The correct answer is {1}.", moduleId, correctAnswer.ToString());
        else
            Debug.LogFormat("[Levenshtein Distance #{0}] The correct answer, after modifications regarding edgework, modulo 100 is {1}.", moduleId, correctAnswer.ToString());
    }

    void Swap(ref int x, ref int y)
    {
        int temp = x;
        x = y;
        y = temp;
    }
    int GetMin(int x, int y, int z)
    {
        int min = x;

        if (min > y)
            min = y;

        if (min > z)
            return z;

        return min;
    }
    void PrintLogRow(bool border, int moduleId)
    {
        StringBuilder sb = new StringBuilder();
        int length = possibleWords[chosenWordsIndeces[1]].Length;

        if (!border)
            sb.Append(separatorLeftRight);
        else
            sb.Append(space);

        sb.Append(separatorTopBottom);
        for (int i = 0; i <= length; i++)
        {
            sb.Append(separatorBoth);
            sb.Append(separatorTopBottom);
        }

        if (!border)
            sb.Append(separatorLeftRight);
        else
            sb.Append(space);

        Debug.LogFormat("[Levenshtein Distance #{0}] {1}", moduleId, sb.ToString());
    }
    int CalculateLevenshteinDistance()
    {
        string input1 = possibleWords[chosenWordsIndeces[0]];
        string input2 = possibleWords[chosenWordsIndeces[1]];
        int m = input1.Length;
        int n = input2.Length;

        // Logging:
        Debug.LogFormat("[Levenshtein Distance #{0}] Using dynamic programming. The matrix for words {1} and {2}:", moduleId, input1, input2);

        PrintLogRow(true, moduleId);

        StringBuilder sb = new StringBuilder();
        sb.Append(separatorLeftRight);
        sb.Append(space);
        sb.Append(separatorLeftRight);
        sb.Append(space);
        sb.Append(separatorLeftRight);
        for (int i = 0; i <= n - 1; i++)
        {
            sb.Append(input2[i]);
            sb.Append(separatorLeftRight);
        }
        Debug.LogFormat("[Levenshtein Distance #{0}] {1}", moduleId, sb.ToString());
        sb = new StringBuilder();

        // Algorithm:
        int[] row0 = new int[n + 1];
        int[] row1 = new int[n + 1];


        PrintLogRow(false, moduleId);
        sb.Append(separatorLeftRight);
        sb.Append(space);
        sb.Append(separatorLeftRight);

        int insertCost, deleteCost, substituteCost;
        for (int i = 0; i <= n; i++)
        {
            row0[i] = i;
            sb.Append(i);
            sb.Append(separatorLeftRight);
        }
        Debug.LogFormat("[Levenshtein Distance #{0}] {1}", moduleId, sb.ToString());
        sb = new StringBuilder();

        for (int i = 0; i <= m - 1; i++)
        {
            PrintLogRow(false, moduleId);
            sb.Append(separatorLeftRight);
            sb.Append(input1[i]);
            sb.Append(separatorLeftRight);

            row1[0] = i + 1;
            sb.Append(row1[0]);
            sb.Append(separatorLeftRight);

            for (int j = 0; j <= n - 1; j++)
            {
                deleteCost = row0[j + 1] + 1;
                insertCost = row1[j] + 1;
                if (input1[i] == input2[j])
                    substituteCost = row0[j];
                else
                    substituteCost = row0[j] + 1;

                row1[j + 1] = GetMin(insertCost, deleteCost, substituteCost);
                sb.Append(row1[j + 1]);
                sb.Append(separatorLeftRight);
            }
            Debug.LogFormat("[Levenshtein Distance #{0}] {1}", moduleId, sb.ToString());
            sb = new StringBuilder();

            for (int j = 0; j <= n; j++)
                Swap(ref row0[j], ref row1[j]);
        }
        PrintLogRow(true, moduleId);


        return row0[n];
    }
    int CountPortTypes()
    {
        List<string> ports = new List<string>();

        foreach (string port in Bomb.GetPorts())
        {
            if (!ports.Contains(port))
                ports.Add(port);
        }

        return ports.Count;
    }
    int DetermineCorrectAnswer(out int levDistance)
    {
        int answer;

        levDistance = CalculateLevenshteinDistance();
        answer = levDistance;

        // Edgework modifications:
        if (Bomb.GetSerialNumberLetters().Contains('L') && Bomb.GetSerialNumberLetters().Contains('D'))
        {
            LDinSerial = true;
            return answer;
        }
        else
        {
            int batteries = Bomb.GetBatteryCount();
            answer *= (batteries == 0 ? 1 : batteries);
            answer += (Bomb.GetIndicators().Count() * CountPortTypes());
        }

        return answer % 100;
    }

    void PickWords()
    {
        int index;
        for (int i = 0; i <= 1; i++)
        {
            index = UnityEngine.Random.Range(0, possibleWords.Length);
            chosenWordsIndeces[i] = index;
            inputWords[i].text = possibleWords[index];
        }


        answerText.text = "";
    }

    void DigitButtonPressed(KMSelectable digit)
    {
        PlaySoundButtonPressed();

        if (moduleSolved)
            return;

        if (answerText.text.Length == maxDigits)
            return;

        digit.AddInteractionPunch();

        string displayedText = answerText.text;
        string pressedDigit = digit.GetComponentInChildren<TextMesh>().text;

        if (displayedText == "0")
            answerText.text = pressedDigit;
        else
            answerText.text += pressedDigit;
    }
    void DeleteLastDigit()
    {
        if (moduleSolved)
            return;
        buttonDel.AddInteractionPunch();
        if (answerText.text.Length > 0)
            answerText.text = answerText.text.Substring(0, answerText.text.Length - 1);
    }
    void ClearDisplay()
    {
        if (moduleSolved)
            return;
        buttonClr.AddInteractionPunch();
        if (answerText.text.Length > 0)
            answerText.text = "";
    }
    void Submit()
    {
        if (moduleSolved)
            return;
        buttonSub.AddInteractionPunch();
        string submittedAnswer = answerText.text;
        Debug.LogFormat("[Levenshtein Distance #{0}] You submitted \"{1}\". The correct answer is \"{2}\".", moduleId, submittedAnswer, correctAnswer.ToString());
        int answer;
        if (int.TryParse(submittedAnswer, out answer) && correctAnswer == answer)
        {
            // Solved.
            moduleSolved = true;
            GetComponent<KMBombModule>().HandlePass();
            inputWords[0].text = "MODULE";
            inputWords[1].text = "HAS BEEN";
            answerText.text = "SOLVED :)";

            Debug.LogFormat("[Levenshtein Distance #{0}] Module solved.", moduleId);
        }
        else
        {
            // Strikes.
            GetComponent<KMBombModule>().HandleStrike();
            answerText.text = "";

            Debug.LogFormat("[Levenshtein Distance #{0}] Strike! Incorrect answer.", moduleId);
        }
    }
    void PlaySoundButtonPressed()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
    }

    void PressButtonClr()
    {
        PlaySoundButtonPressed();
        ClearDisplay();
    }
    void PressButtonDel()
    {
        PlaySoundButtonPressed();
        DeleteLastDigit();
    }
    void PressButtonSub()
    {
        PlaySoundButtonPressed();
        Submit();
    }

#pragma warning disable 0414
    private readonly string TwitchHelpMessage = "!{0} submit <##> [Submits the given number as the answer. Answer must be 1 or 2 digits long.]";
#pragma warning restore 0414

    private IEnumerator ProcessTwitchCommand(string command)
    {
        var m = Regex.Match(command, @"^\s*submit\s+([0-9\s]+)\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
        if (!m.Success)
            yield break;  
        yield return null;
        var cmd = m.Groups[1].Value.ToString();
        Debug.Log(cmd);
        if (cmd.Length > 2)
        {
            yield return "sendtochaterror Your answer must be only 1 or 2 digits long!";
            yield break;
        }
        buttonClr.OnInteract();
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < cmd.Length; i++)
        {
            digits[int.Parse(cmd.Substring(i, 1))].OnInteract();
            yield return new WaitForSeconds(0.1f);
        }
        buttonSub.OnInteract();
        yield break;
    }
    
    private IEnumerator TwitchHandleForcedSolve()
    {
        var a = correctAnswer.ToString();
        buttonClr.OnInteract();
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < a.Length; i++)
        {
            digits[int.Parse(a.Substring(i, 1))].OnInteract();
            yield return new WaitForSeconds(0.1f);
        }
        buttonSub.OnInteract();
        yield break;
    }
}
