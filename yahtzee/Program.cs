using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace yahtzee
{
    class YahtzeeMain
    /// Main class where we declare players, introduce the game, go through the main game loop and conclude. 
    {
        public static void Main()
        {
            ///Introduction
            Console.WriteLine("Welcome to Yahztee!");
            Console.WriteLine("");
            Console.WriteLine("The objective of Yahtzee is to get as many points as possible by rolling five dice and getting" +
                              " certain combinations of dice.");
            Console.WriteLine("");
            Console.WriteLine("Enter q or quit at any time to quit the game.");
            Console.WriteLine("");
            var player1 = new Human();
            var player2 = new AI2();
            Console.WriteLine("You will be playing against the computer, my name is {0}!", player2.GetName());
            Console.WriteLine("");
            Console.WriteLine("Press enter to begin!");
            Console.ReadKey();

            /// Main Game Loop

            while (player1.PlayerFinished() == false && player2.PlayerFinished() == false)
            {
                Console.Clear();
                PrintCard(player1, player2);
                player1.TakeTurn();
                player2.TakeTurn();
            }
            PrintCard(player1, player2);
            GameOver(player1, player2);
            Quit();
            /// Write function that goes here and calculates who had the higher score and prints who won the game.
        }

        public static void PrintCard(player player1, player player2)
        ///Method to print score card to the console. Currently hard coded to take two players (human v human, human v AI, AI vs AI.
        {
            int[] scores1 = player1.GetScore();
            int[] scores2 = player2.GetScore();
            Console.WriteLine("     Roll       |         Rule       |Slot|{0}'s Score\t|{1}'s Score", player1.GetName(), player2.GetName());
            Console.WriteLine("Ones = 1        |Count and add ones  | 1  |\t" + View(scores1[0]) + "\t|\t" + View(scores2[0]));
            Console.WriteLine("Twos = 2        |Count and add twos  | 2  |\t" + View(scores1[1]) + "\t|\t" + View(scores2[1]));
            Console.WriteLine("Threes = 3      |Count and add threes| 3  |\t" + View(scores1[2]) + "\t|\t" + View(scores2[2]));
            Console.WriteLine("Fours =  4      |Count and add fours | 4  |\t" + View(scores1[3]) + "\t|\t" + View(scores2[3]));
            Console.WriteLine("Fives =  5      |Count and add fives | 5  |\t" + View(scores1[4]) + "\t|\t" + View(scores2[4]));
            Console.WriteLine("Sixes =  6      |Count and add sixes | 6  |\t" + View(scores1[5]) + "\t|\t" + View(scores2[5]));
            Console.WriteLine("Upper SubTotal ---------------------------->\t" + View(scores1[6]) + "\t|\t" + View(scores2[6]));
            Console.WriteLine("Add 35 if SubTotal is > 63 ---------------->\t" + View(scores1[7]) + "\t|\t" + View(scores2[7]));
            Console.WriteLine("Upper Total ------------------------------->\t" + View(scores1[8]) + "\t|\t" + View(scores2[8]));
            Console.WriteLine("Three of a kind |Total of all dice   | 7  |\t" + View(scores1[9]) + "\t|\t" + View(scores2[9]));
            Console.WriteLine("Four of a kind  |Total of all dice   | 8  |\t" + View(scores1[10]) + "\t|\t" + View(scores2[10]));
            Console.WriteLine("Full house      |Score 25            | 9  |\t" + View(scores1[11]) + "\t|\t" + View(scores2[11]));
            Console.WriteLine("Small straight  |Score 30            | 10 |\t" + View(scores1[12]) + "\t|\t" + View(scores2[12]));
            Console.WriteLine("Large Straight  |Score 40            | 11 |\t" + View(scores1[13]) + "\t|\t" + View(scores2[13]));
            Console.WriteLine("Chance          |Total of all dice   | 12 |\t" + View(scores1[14]) + "\t|\t" + View(scores2[14]));
            Console.WriteLine("Yahtzee         |Score 50            | 13 |\t" + View(scores1[15]) + "\t|\t" + View(scores2[15]));
            Console.WriteLine("Lower Total ------------------------------->\t" + View(scores1[16]) + "\t|\t" + View(scores2[16]));
            Console.WriteLine("Grand Total ------------------------------->\t" + View(scores1[17]) + "\t|\t" + View(scores2[17]));
            Console.WriteLine("\n");
        }

        public static string View(int score)
        /// Method View makes sure to only display a score on the Score Card if there has been a value added. All scores start as -1, this
        /// makes it easy to check if the slot is full and when the card is full (just make sure everything is greater than or equal to 0).
        /// Prints 2 underscores if the slot has not been filled yet so you can easily see which slots are open.
        {
            if (score < 0)
            {
                return "__";
            }
            else
            {
                return "" + score;
            }
        }

        public static void GameOver(player player1, player player2)
        {
            int[] scores1 = player1.GetScore();
            int[] scores2 = player2.GetScore();
            Console.WriteLine("{0}'s score: {1}", player1.GetName(), scores1[17]);
            Console.WriteLine("{0}'s score: {1} ", player2.GetName(), scores2[17]);
            if (scores1[17] > scores2[17])
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("You Win!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You Lose!");
            }
            Console.WriteLine("Game Over!");
        }

        public static void Quit()
        {
            Console.WriteLine("\nThank You for Playing Yahtzee!");
            Console.WriteLine("Press any key to Exit!!");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}

public class Scorecard
/// Scorecard class holds all of the scores a player can have and also calculates the scores of each roll and other functions
/// of the scorecard. All scorecard class operates the same whether for a Human or AI.
{
    private int ones;
    private int twos;
    private int threes;
    private int fours;
    private int fives;
    private int sixes;
    private int threeOfAKind;
    private int fourOfAKind;
    private int fullHouse;
    private int smallStraight;
    private int largeStraight;
    private int chance;
    private int yahtzee;

    public Scorecard()
    {
        ones = -1;
        twos = -1;
        threes = -1;
        fours = -1;
        fives = -1;
        sixes = -1;
        threeOfAKind = -1;
        fourOfAKind = -1;
        fullHouse = -1;
        smallStraight = -1;
        largeStraight = -1;
        chance = -1;
        yahtzee = -1;
    }

    public int[] GetScore()
    //Checks current score. Each player has a method that calls this method to look at their personal scorecard.
    {
        int[] currentScore = new int[18];
        currentScore[0] = ones;
        currentScore[1] = twos;
        currentScore[2] = threes;
        currentScore[3] = fours;
        currentScore[4] = fives;
        currentScore[5] = sixes;
        currentScore[6] = UpperTotal();
        currentScore[7] = Bonus();
        currentScore[8] = (UpperTotal() + Bonus());
        currentScore[9] = threeOfAKind;
        currentScore[10] = fourOfAKind;
        currentScore[11] = fullHouse;
        currentScore[12] = smallStraight;
        currentScore[13] = largeStraight;
        currentScore[14] = chance;
        currentScore[15] = yahtzee;
        currentScore[16] = LowerTotal();
        currentScore[17] = (GrandTotal() + Bonus());

        return currentScore;
    }

    public bool SlotOpen(int slot)
    //Method checks to see if a slot is open.
    {
        if (slot == 1 && ones == -1)
        {
            return true;
        }
        else if (slot == 2 && twos == -1)
        {
            return true;
        }
        else if (slot == 3 && threes == -1)
        {
            return true;
        }
        else if (slot == 4 && fours == -1)
        {
            return true;
        }
        else if (slot == 5 && fives == -1)
        {
            return true;
        }
        else if (slot == 6 && sixes == -1)
        {
            return true;
        }
        else if (slot == 7 && threeOfAKind == -1)
        {
            return true;
        }
        else if (slot == 8 && fourOfAKind == -1)
        {
            return true;
        }
        else if (slot == 9 && fullHouse == -1)
        {
            return true;
        }
        else if (slot == 10 && smallStraight == -1)
        {
            return true;
        }
        else if (slot == 11 && largeStraight == -1)
        {
            return true;
        }
        else if (slot == 12 && chance == -1)
        {
            return true;
        }
        else if (slot == 13 && yahtzee == -1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int SetScore(int[] dice, int slot)
    ///After concluding the slot is open, SetScore takes the dice array and calls all the scoring methods to put the correct
    ///score in the corresponding slot. This changes the value of the variable for that scorecard attached to that player
    /// moving forward.
    {
        if (slot == 1)
        {
            ones = ScoreUpper(dice, 1);
        }
        else if (slot == 2)
        {
            twos = ScoreUpper(dice, 2);
        }
        else if (slot == 3)
        {
            threes = ScoreUpper(dice, 3);
        }
        else if (slot == 4)
        {
            fours = ScoreUpper(dice, 4);
        }
        else if (slot == 5)
        {
            fives = ScoreUpper(dice, 5);
        }
        else if (slot == 6)
        {
            sixes = ScoreUpper(dice, 6);
        }
        else if (slot == 7)
        {
            threeOfAKind = ScoreThreeOfAKind(dice);
        }
        else if (slot == 8)
        {
            fourOfAKind = ScoreFourOfAKind(dice);
        }
        else if (slot == 9)
        {
            fullHouse = ScoreFullHouse(dice);
        }
        else if (slot == 10)
        {
            smallStraight = ScoreSmallStraight(dice);
        }
        else if (slot == 11)
        {
            largeStraight = ScoreLargeStraight(dice);
        }
        else if (slot == 12)
        {
            chance = ScoreChance(dice);
        }
        else if (slot == 13)
        {
            yahtzee = ScoreYahtzee(dice);
        }
        else
        {
            return 0;
        }
        return 0;
    }

    public int ScoreUpper(int[] dice, int dieNumber)
    /// Method to score all of the upper section of the score card. Needs the dice and the die number that the player is choosing
    /// to pass in in order to score.
    {
        int score = 0;
        for (int i = 0; i < dice.Length; i++)
        {
            if (dice[i] == dieNumber)
            {
                score += dieNumber;
            }
        }
        return score;
    }

    public int Bonus()
    ///Bonus method adds 35 to the upper total if the subtotal is greater thanor equal to 63.
    {
        int yahtzeeBonus = 0;

        if (UpperTotal() >= 63)
        {
            yahtzeeBonus = 35;
        }
        else
        {
            yahtzeeBonus = 0;
        }

        return yahtzeeBonus;
    }


    public int ScoreThreeOfAKind(int[] dice)
    /// Method to calculate the score of these dice in the three of a kind slot.
    {
        int sum = 0;

        bool threeOfAKindSum = false;

        for (int i = 1; i <= 6; i++)
        {
            for (int j = 0; j < dice.Length - 2; j++)
            {
                if (dice[j] == i && dice[j + 1] == i && dice[j + 2] == i)
                {
                    threeOfAKindSum = true;
                }
            }
        }

        if (threeOfAKindSum)
        {
            for (int k = 0; k < 5; k++)
            {
                sum += dice[k];
            }
        }
        return sum;
    }


    public int ScoreFourOfAKind(int[] dice)
    /// Method to calculate the score of these dice in the four of a kind slot.
    {
        int sum = 0;

        bool fourOfAKindSum = false;

        for (int i = 1; i <= 6; i++)
        {
            for (int j = 0; j < dice.Length - 3; j++)
            {
                if (dice[j] == i && dice[j + 1] == i && dice[j + 2] == i && dice[j + 3] == i)
                {
                    fourOfAKindSum = true;
                }
            }
        }

        if (fourOfAKindSum)
        {
            for (int k = 0; k < 5; k++)
            {
                sum += dice[k];
            }
        }
        return sum;
    }

    public int ScoreFullHouse(int[] dice)
    /// Method to calculate the score of these dice in the full house slot.
    {
        int fullHouseSum = 0;
        if ((((dice[0] == dice[1]) && (dice[1] == dice[2])) && (dice[3] == dice[4]) && (dice[2] != dice[3]))
                                                    ||
            ((dice[0] == dice[1]) && ((dice[2] == dice[3]) && (dice[3] == dice[4])) && (dice[1] != dice[2])))
        {
            fullHouseSum = 25;
        }
        else
        {
            fullHouseSum = 0;
        }

        return fullHouseSum;
    }

    public int ScoreSmallStraight(int[] dice)
    /// Method to calculate the score of these dice in the small straight slot.
    {
        int counter = 0;
        int smallStraightSum = 0;
        bool found = false;

        for (int i = 0; i < dice.Length - 1; i++)
        {
            if (dice[i + 1] == dice[i] + 1)
            {
                counter++;
            }
            else if (dice[i + 1] == dice[i])
            {
                continue;
            }
            else
            {
                counter = 0;
            }
            if (counter == 3)
            {
                found = true;
                break;
            }
        }

        if (found)
        {
            smallStraightSum = 30;
        }
        else
        {
            smallStraightSum = 0;
        }

        return smallStraightSum;
    }


    public int ScoreLargeStraight(int[] dice)
    /// Method to calculate the score of these dice in the large straight slot.
    {
        int counter = 0;
        int largeStraightSum = 0;
        bool found = false;

        for (int i = 0; i < dice.Length - 1; i++)
        {
            if (dice[i + 1] == dice[i] + 1)
            {
                counter++;
            }
            else if (dice[i + 1] == dice[i])
            {
                continue;
            }
            else
            {
                counter = 0;
            }
            if (counter == 4)
            {
                found = true;
                break;
            }
        }

        if (found)
        {
            largeStraightSum = 40;
        }
        else
        {
            largeStraightSum = 0;
        }

        return largeStraightSum; ;
    }

    public int ScoreChance(int[] dice)
    /// Method to calculate the score of these dice in the chance slot.
    {
        int sum = 0;

        for (int i = 0; i < 5; i++)
        {
            sum += dice[i];
        }
        return sum;
    }

    public int ScoreYahtzee(int[] dice)
    /// Method to calculate the score of these dice in the yahtzee slot.
    {
        int yahtzeeSum = 0;
        if (dice[0] == dice[1] && dice[1] == dice[2] && dice[2] == dice[3] && dice[3] == dice[4])
        {
            yahtzeeSum = 50;
        }
        else
        {
            yahtzeeSum = 0;
        }

        return yahtzeeSum;
    }

    public int UpperTotal()
    /// Method to calculate the sub total of the upper section of the card.
    {
        int sum = 0;

        if (ones != -1)
        {
            sum += ones;
        }
        if (twos != -1)
        {
            sum += twos;
        }
        if (threes != -1)
        {
            sum += threes;
        }
        if (fours != -1)
        {
            sum += fours;
        }
        if (fives != -1)
        {
            sum += fives;
        }
        if (sixes != -1)
        {
            sum += sixes;
        }

        return sum;
    }

    public int LowerTotal()
    /// Method to calculate the total of the lower section of the card.
    {
        int sum = 0;

        if (threeOfAKind != -1)
        {
            sum += threeOfAKind;
        }
        if (fourOfAKind != -1)
        {
            sum += fourOfAKind;
        }
        if (fullHouse != -1)
        {
            sum += fullHouse;
        }
        if (smallStraight != -1)
        {
            sum += smallStraight;
        }
        if (largeStraight != -1)
        {
            sum += largeStraight;
        }
        if (chance != -1)
        {
            sum += chance;
        }
        if (yahtzee != -1)
        {
            sum += yahtzee;
        }
        return sum;
    }

    public int GrandTotal()
    /// Method to calculate the total of the upper and lower sections(includes the bonus on the print card function)
    {
        int sum = 0;

        if (ones != -1)
        {
            sum += ones;
        }
        if (twos != -1)
        {
            sum += twos;
        }
        if (threes != -1)
        {
            sum += threes;
        }
        if (fours != -1)
        {
            sum += fours;
        }
        if (fives != -1)
        {
            sum += fives;
        }
        if (sixes != -1)
        {
            sum += sixes;
        }
        if (threeOfAKind != -1)
        {
            sum += threeOfAKind;
        }
        if (fourOfAKind != -1)
        {
            sum += fourOfAKind;
        }
        if (fullHouse != -1)
        {
            sum += fullHouse;
        }
        if (smallStraight != -1)
        {
            sum += smallStraight;
        }
        if (largeStraight != -1)
        {
            sum += largeStraight;
        }
        if (chance != -1)
        {
            sum += chance;
        }
        if (yahtzee != -1)
        {
            sum += yahtzee;
        }
        return sum;
    }
}

   interface player
/// Interface for having different types of players. This allows a player to be human or AI and takes all the methods
/// that the player interacts with.
{
    void TakeTurn();
    int[] GetScore();
    string GetName();
    bool PlayerFinished();
}

public class Human : player
/// Human Player has multiple prompts for decisions. 
{
    private string name;
    private Scorecard score;
    static Random random = new Random();

    public Human()
    {
        this.name = UI.PromptLine("Please enter your name: ");
        this.score = new Scorecard();
    }

    public void TakeTurn()
    /// TakeTurn is used in the main loop. It is different for Human v AI because the Human player will choose which dice 
    /// to hold and which slot to set the score by different conventions than the AI. This method rolls the dice three times,
    /// asks where to put the score, makes sure that slot is open, then sets the score the open slot.
    {
        int[] dice = new int[5];
        Console.WriteLine("{0}'s turn!\n", name);
        Console.WriteLine("{0} roll 1: ", name);
        RollDice(dice);
        Console.WriteLine("");
        Console.WriteLine("{0} roll 2: ", name);
        ReRoll(dice);
        Console.WriteLine("");
        Console.WriteLine("{0} roll 3: ", name);
        ReRoll(dice);
        Console.WriteLine("");
        Console.WriteLine("Please enter 0 to quit!");
        Console.WriteLine("Please enter which slot you would like to put your score: ");
        int slot = UI.PromptInt("Please enter which slot you would like to put your score: ");
        while (score.SlotOpen(slot) == false)
        {
            if (slot == 0)
            {
                Quit();
            }
            else
            {
                slot = UI.PromptInt("Please enter a valid slot number: ");
            }
        }
        score.SetScore(dice, slot);
        Console.WriteLine("\n");
    }

    public int[] GetScore()
    /// Turns all the scores from the score card into one array for the player. Uses GetScore method from
    /// scorecard class. Same for Human and AI.
    {
        return score.GetScore();
    }

    public string GetName()
    /// Returns the private name to other classes. Same for Human and AI
    {
        return name;
    }

    public bool PlayerFinished()
    /// Checks all current scores in the main game loop after every turn to see if the game is over. If any score is still -1
    /// the game is not over.
    {
        int[] currentScore = GetScore();
        if (currentScore[0] >= 0 && currentScore[1] >= 0 && currentScore[2] >= 0 && currentScore[3] >= 0 &&
            currentScore[4] >= 0 && currentScore[5] >= 0 && currentScore[9] >= 0 &&
            currentScore[10] >= 0 && currentScore[11] >= 0 && currentScore[12] >= 0 && currentScore[13] >= 0 &&
            currentScore[14] >= 0 && currentScore[15] >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static int[] RollDice(int[] dice)
    {
        for (int i = 0; i < 5; i++)
        {
            dice[i] = random.Next(1, 7);
        }
        //let's sort array in increasing order
        Array.Sort(dice);

        char[] chars = { 'A', 'B', 'C', 'D', 'E' };
        for (int j = 0; j < dice.Length; j++)
        {
            Console.Write("{0}: {1}  ", chars[j], dice[j]);
        }
        Console.WriteLine("");
        return dice;
    }

    public int[] ReRoll(int[] dice) //method to re-roll certain dice
    {

        while (true)
        {
            Console.WriteLine("\nPlease enter any letter from A to E to reroll that die");
            string letters = UI.PromptLine("Please enter letters to reroll: ");
            var userInputLetters = letters.Replace(" ", "").ToUpper();
            if (userInputLetters == "Q" || userInputLetters == "QUIT")
            {
                break;
            }
            else
            {
                var userInputList = userInputLetters.ToCharArray().ToList();
                userInputList = userInputList.Distinct().ToList();
                Console.WriteLine("");

                Random r = new Random();
                var diceValue = r.Next(1, 7);
                for (var i = 0; i < userInputList.Count; i++)
                {
                    if (userInputList[i] == 'A')
                    {
                        dice[0] = diceValue;
                    }
                    else if (userInputList[i] == 'B')
                    {
                        dice[1] = diceValue;
                    }
                    else if (userInputList[i] == 'C')
                    {
                        dice[2] = diceValue;
                    }
                    else if (userInputList[i] == 'D')
                    {
                        dice[3] = diceValue;
                    }
                    else if (userInputList[i] == 'E')
                    {
                        dice[4] = diceValue;
                    }
                }
                //let's sort array in increasing order
                Array.Sort(dice);
                char[] chars = { 'A', 'B', 'C', 'D', 'E' };
                for (int j = 0; j < dice.Length; j++)
                {
                    Console.Write("{0}: {1}  ", chars[j], dice[j]);
                }
                Console.WriteLine("");
            }
            return dice;
        }
        Quit();
        return dice;
    }

    public void Quit()
    {
        Console.WriteLine("\nThank You for Playing Yahtzee!");
        Console.WriteLine("Press any key to Exit!!");
        Console.ReadKey();
        Environment.Exit(0);
    }
}

public class AI2 : player
/// AI has hard coded decisions that use logic. Instead of giving this AI a choice of what dice to hold we give her 2 extra rolls to 
/// choose from, she then calculates which has the best max potential for points of the five rolls and sets the slot.
{
    private string name;
    private Scorecard score;
    static Random random = new Random();

    public AI2()
    {
        this.name = "Roxeanne";
        this.score = new Scorecard();
    }

    public void TakeTurn()
    ///TakeTurn for AI is giving the AI five rolls. Each roll is evaluated to see which slot is the max potential for points for that 
    /// roll, all the max points are then compared and the highest is chosen to be set in a slot. This should be re-written as a loop.
    {
        int[] dice = new int[5];
        Console.WriteLine("{0}'s turn!\n", name);
        Console.WriteLine("{0} roll 1: ", name);
        RollDice(dice);
        Console.WriteLine("");
        Thread.Sleep(1000);
        Console.WriteLine("{0} roll 2: ", name);
        ReRoll(dice);
        Console.WriteLine("");
        Thread.Sleep(1000);
        Console.WriteLine("{0} roll 3: ", name);
        ReRoll(dice);
        Console.WriteLine("");
        Thread.Sleep(1000);
        Console.Write("Please enter which slot you would like to put your score: ");
        int slot = EvaluateMaximumScore(dice);
        Console.WriteLine(slot);
        score.SetScore(dice, slot);
        Console.WriteLine("");
        Console.WriteLine("Press enter to take your turn!");
        Console.ReadKey();
    }

    public int[] GetScore()
    /// Turns all the scores from the score card into one array for the player. Uses GetScore method from
    /// scorecard class. Same for Human and AI.
    {
        return score.GetScore();
    }

    public string GetName()
    /// Returns the private name to other classes. Same for Human and AI
    {
        return name;
    }

    private int EvaluateMaximumScore(int[] dice)
    /// EvaluateMaximumScore is not used in this AI, but was used for an earlier AI that only got one roll. May use this
    /// for an "easy mode". Also known as pessimistic AI, RIP Bill. Simpler version of BestSlotValue which returns a dictionary.
    {
        int ones = score.ScoreUpper(dice, 1);
        int twos = score.ScoreUpper(dice, 2);
        int threes = score.ScoreUpper(dice, 3);
        int fours = score.ScoreUpper(dice, 4);
        int fives = score.ScoreUpper(dice, 5);
        int sixes = score.ScoreUpper(dice, 6);
        int threeOfAKind = score.ScoreThreeOfAKind(dice);
        int fourOfAKind = score.ScoreFourOfAKind(dice);
        int fullHouse = score.ScoreFullHouse(dice);
        int smallStraight = score.ScoreSmallStraight(dice);
        int largeStraight = score.ScoreLargeStraight(dice);
        int chance = score.ScoreChance(dice);
        int yahtzee = score.ScoreYahtzee(dice);
        int max = 0;
        int slot = 0;

        if (ones >= max && score.SlotOpen(1))
        {
            max = ones;
            slot = 1;
        }
        if (twos >= max && score.SlotOpen(2))
        {
            max = twos;
            slot = 2;
        }
        if (threes >= max && score.SlotOpen(3))
        {
            max = threes;
            slot = 3;
        }
        if (fours >= max && score.SlotOpen(4))
        {
            max = fours;
            slot = 4;
        }
        if (fives >= max && score.SlotOpen(5))
        {
            max = fives;
            slot = 5;
        }
        if (sixes >= max && score.SlotOpen(6))
        {
            max = sixes;
            slot = 6;
        }
        if (threeOfAKind >= max && score.SlotOpen(7))
        {
            max = threeOfAKind;
            slot = 7;
        }
        if (fourOfAKind >= max && score.SlotOpen(8))
        {
            max = fourOfAKind;
            slot = 8;
        }
        if (fullHouse >= max && score.SlotOpen(9))
        {
            max = fullHouse;
            slot = 9;
        }
        if (smallStraight >= max && score.SlotOpen(10))
        {
            max = smallStraight;
            slot = 10;
        }
        if (largeStraight >= max && score.SlotOpen(11))
        {
            max = largeStraight;
            slot = 11;
        }
        if (chance >= max && score.SlotOpen(12))
        {
            max = chance;
            slot = 12;
        }
        if (yahtzee >= max && score.SlotOpen(13))
        {
            max = yahtzee;
            slot = 13;
        }
        return slot;
    }

    public bool PlayerFinished()
    /// Checks all current scores in the main game loop after every turn to see if the game is over. If any score is still -1
    /// the game is not over.
    {
        int[] currentScore = GetScore();
        if (currentScore[0] >= 0 && currentScore[1] >= 0 && currentScore[2] >= 0 && currentScore[3] >= 0 &&
            currentScore[4] >= 0 && currentScore[5] >= 0 && currentScore[9] >= 0 &&
            currentScore[10] >= 0 && currentScore[11] >= 0 && currentScore[12] >= 0 && currentScore[13] >= 0 &&
            currentScore[14] >= 0 && currentScore[15] >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static int[] RollDice(int[] dice)
    /// Creates a random array of 5 dice that emulate a dice roll.
    {
        for (int i = 0; i < 5; i++)
        {
            dice[i] = random.Next(1, 7);
        }
        Array.Sort(dice);
        char[] chars = { 'A', 'B', 'C', 'D', 'E' };
        for (int j = 0; j < dice.Length; j++)
        {
            Console.Write("{0}: {1}  ", chars[j], dice[j]);
        }
        Console.WriteLine("");

        return dice;
    }

    public static int[] ReRoll(int[] dice) //method to re-roll certain dice
    {
        Console.WriteLine("\nPlease enter any letter from A to E to reroll that die");
        Console.Write("Please enter letters to reroll: ");
        Console.WriteLine(AI2.HoldDice(dice));
        string letters = AI2.HoldDice(dice);
        var userInputLetters = letters.Replace(" ", "").ToUpper();
        var userInputList = userInputLetters.ToCharArray().ToList();
        userInputList = userInputList.Distinct().ToList();

        var diceValue = random.Next(1, 7);
        for (var i = 0; i < userInputList.Count; i++)
        {
            if (userInputList[i] == 'A')
            {
                dice[0] = diceValue;
            }
            else if (userInputList[i] == 'B')
            {
                dice[1] = diceValue;
            }
            else if (userInputList[i] == 'C')
            {
                dice[2] = diceValue;
            }
            else if (userInputList[i] == 'D')
            {
                dice[3] = diceValue;
            }
            else if (userInputList[i] == 'E')
            {
                dice[4] = diceValue;
            }
        }

        char[] chars = { 'A', 'B', 'C', 'D', 'E' };
        Array.Sort(dice);
        for (int j = 0; j < dice.Length; j++)
        {
            Console.Write("{0}: {1}  ", chars[j], dice[j]);
        }
        Console.WriteLine("");
        return dice;
    }

    public static string HoldDice(int[] dice)
    {
        if (dice[0] == dice[1] && dice[1] == dice[2] && dice[2] == dice[3] && dice[3] == dice[4])
        {
            return "";
        }
        else if (((dice[0] == dice[1]) && (dice[1] == dice[2])) && (dice[3] == dice[4]) && (dice[2] != dice[3]))
        {
            return "";
        }
        else if ((dice[0] == dice[1]) && ((dice[2] == dice[3]) && (dice[3] == dice[4])) && (dice[1] != dice[2]))
        {
            return "";
        }
        else if (dice[0] == dice[1] && dice[1] == dice[2] && dice[2] == dice[3])
        {
            return "E";
        }
        else if (dice[1] == dice[2] && dice[2] == dice[3] && dice[3] == dice[4])
        {
            return "A";
        }
        else if (dice[0] == dice[1] && dice[1] == dice[2])
        {
            return "DE";
        }
        else if (dice[1] == dice[2] && dice[2] == dice[3])
        {
            return "AE";
        }
        else if (dice[2] == dice[3] && dice[3] == dice[4])
        {
            return "AB";
        }
        else if (dice[0] == dice[1])
        {
            return "CDE";
        }
        else if (dice[1] == dice[2])
        {
            return "ADE";
        }
        else if (dice[2] == dice[3])
        {
            return "ABE";
        }
        else if (dice[3] == dice[4])
        {
            return "ABC";
        }
        else
        {
            return "ABCDE";
        }
    }

    public void AIFinalChoice(int[] finalDice, int finalSlot)
    {
        Console.Write("{0}'s roll: ", name);
        for (int j = 0; j < finalDice.Length; j++)
        {
            Console.Write("{0} ", finalDice[j]);
        }
        Console.WriteLine("\n");
        Console.WriteLine("{0} chooses slot {1}.", name, finalSlot);
        Console.WriteLine("\n");
    }
}

public class UI
{
    /// After displaying the prompt, return a line from the keyboard.
    public static string PromptLine(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }

    /// Prompt the user to enter an integer until the response is legal.
    /// Return the result as in int.
    public static int PromptInt(string prompt)
    {
        string nStr = PromptLine(prompt).Trim();
        while (!IsIntString(nStr))
        {
            Console.WriteLine("Bad int format!  Try again.");
            nStr = PromptLine(prompt).Trim();
        }
        return int.Parse(nStr);
    }

    /// Prompt the user to enter a decimal value until the response
    /// is legal.  Return the result as a double.
    public static double PromptDouble(string prompt)
    {
        string nStr = PromptLine(prompt).Trim();
        while (!IsDecimalString(nStr))
        {
            Console.WriteLine("Bad decimal format!  Try again.");
            nStr = PromptLine(prompt).Trim();
        }
        return double.Parse(nStr);
    }

    /// Prompt the user to enter a decimal value until the response
    /// is legal.  Return the result as a decimal.
    public static decimal PromptDecimal(string prompt)
    {
        string nStr = PromptLine(prompt).Trim();
        while (!IsDecimalString(nStr))
        {
            Console.WriteLine("Bad decimal format!  Try again.");
            nStr = PromptLine(prompt).Trim();
        }
        return decimal.Parse(nStr);
    }

    /// Prompt the user until a keyboard entry is an int
    /// in the range [lowLim, highLim].  Then return the int value
    /// in range.  Append the range to the prompt.
    public static int PromptIntInRange(string prompt,
                                       int lowLim, int highLim)
    {
        string longPrompt = string.Format("{0} ({1} through {2}) ",
                                          prompt, lowLim, highLim);
        int number = PromptInt(longPrompt);
        while (number < lowLim || number > highLim)
        {
            Console.WriteLine("{0} is out of range!", number);
            number = PromptInt(longPrompt);
        }
        return number;
    }

    /// Prompt the user until a keyboard entry is a decimal
    /// in the range [lowLim, highLim].  Then return the double
    /// value in range.  Append the range to the prompt.
    public static double PromptDoubleInRange(string prompt,
          double lowLim, double highLim)
    {
        string longPrompt = string.Format("{0} ({1} through {2}) ",
                                          prompt, lowLim, highLim);
        double number = PromptDouble(longPrompt);
        while (number < lowLim || number > highLim)
        {
            Console.WriteLine("{0} is out of range!", number);
            number = PromptDouble(longPrompt);
        }
        return number;
    }

    /// Prompt the user with a question.
    /// Force an understandable keyboard response;
    /// Return true of false based on the final response.
    public static Boolean Agree(string question)
    {
        string meanYes = "ytYT", meanNo = "nfNF",
               validResponses = meanYes + meanNo;
        string answer = PromptLine(question);
        while (answer.Length == 0 ||
               !validResponses.Contains("" + answer[0]))
        {
            Console.WriteLine("Enter y or n!");
            answer = PromptLine(question);
        }
        return meanYes.Contains("" + answer[0]);
    }

    // helper string testing functions

    /// True when s consists of only 1 or more digits.
    public static bool IsDigits(string s)
    {
        foreach (char ch in s)
        {
            if (ch < '0' || ch > '9')
            {
                return false;
            }
        }
        return (s.Length > 0);
    }

    /// compare integer strings:
    ///   any lengths, but both positive or both negative
    /// true if magnitude of s <= magnitude of lim
    public static bool IntStrMagLessEq(string s, string lim)
    {
        return s.Length < lim.Length || //automatically magnitude less
               s.Length == lim.Length && s.CompareTo(lim) <= 0;
    }     // for same length, lexicographical comparison works


    /// True if s is the string form of an int.
    public static bool IsIntString(string s)
    {
        if (s.StartsWith("-"))
        {
            return IsDigits(s.Substring(1)) &&
                   IntStrMagLessEq(s, "" + int.MinValue);
        }
        return IsDigits(s) && IntStrMagLessEq(s, "" + int.MaxValue);
    }

    /// Return true if s represents a decimal string.
    public static bool IsDecimalString(string s)
    {
        if (s.StartsWith("-"))
        {
            s = s.Substring(1);
        }
        int i = s.IndexOf(".");
        if (i >= 0)
        { //dump found decimal point
            s = s.Substring(0, i) + s.Substring(i + 1);
        }
        return IsDigits(s);
    }
}