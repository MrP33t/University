namespace VignereCipher
{
    public class VignereSolver
    {
        private List<Probability> polishProbabilities;
        public VignereSolver()
        {
            polishProbabilities = new List<Probability>()
            {
                new Probability('A', 8.37),
                new Probability('B', 1.93),
                new Probability('C', 3.89),
                new Probability('D', 3.35),
                new Probability('E', 8.68),
                new Probability('F', 0.26),
                new Probability('G', 1.46),
                new Probability('H', 1.25),
                new Probability('I', 8.83),
                new Probability('J', 2.28),
                new Probability('K', 3.01),
                new Probability('L', 2.24),
                new Probability('M', 2.81),
                new Probability('N', 5.69),
                new Probability('O', 7.53),
                new Probability('P', 2.87),
                new Probability('Q', 0),
                new Probability('R', 4.15),
                new Probability('S', 4.13),
                new Probability('T', 3.85),
                new Probability('U', 2.06),
                new Probability('V', 0),
                new Probability('W', 4.11),
                new Probability('V', 0),
                new Probability('Y', 4.03),
                new Probability('Z', 5.33)
            };
        }

        
        public string SolveVignere(string input, string language)
        {
            if (language == "polish")
                return SolvePolish(input);
            return "Its too hard for me !";
        }

        public int SolveLength(string input)
        {
            var inputWithoutSpecials = string.Empty;
            foreach (char c in input)
            {
                if (char.IsLetter(c))
                    inputWithoutSpecials += c;
            }
            var guessesOfLength = new List<int>();
            for (int i = 1; i <= inputWithoutSpecials.Length; i++)
            {
                var toFind = string.Empty;
                for (int j = 0; j < i; j++)
                {
                    toFind += inputWithoutSpecials[j];
                }

                var index = inputWithoutSpecials.IndexOf(toFind);
                if (index == -1)
                    continue;
                var consistent = true;
                var foundLength = 0;
                while (index >= 0 && consistent)
                {
                    var indexBefore = index;
                    index = inputWithoutSpecials.IndexOf(toFind, index + toFind.Length);
                    if (index == -1)
                        continue;
                    var foundLengthBefore = foundLength;
                    foundLength = index - indexBefore;
                    if (indexBefore == inputWithoutSpecials.IndexOf(toFind))
                        continue;
                    if (foundLengthBefore != foundLength)
                        consistent = false;
                }

                if (consistent && foundLength > 0)
                    guessesOfLength.Add(foundLength);
            }

            return GCD(guessesOfLength);
        }

        static int GCD(List<int> numbers)
        {
            if (numbers.Any()) 
                return numbers.Aggregate(GCD);
            return -1;
        }

        static int GCD(int a, int b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }
        private string SolvePolish(string input)
        {
            var letterCount = 0;
            foreach (char c in input)
            {
                if (char.IsLetter(c))
                    letterCount++;
            }

            var occurences = new List<Occurence>()
            {
                new Occurence('A'),
                new Occurence('B'),
                new Occurence('C'),
                new Occurence('D'),
                new Occurence('E'),
                new Occurence('F'),
                new Occurence('G'),
                new Occurence('H'),
                new Occurence('I'),
                new Occurence('J'),
                new Occurence('K'),
                new Occurence('L'),
                new Occurence('M'),
                new Occurence('N'),
                new Occurence('O'),
                new Occurence('P'),
                new Occurence('Q'),
                new Occurence('R'),
                new Occurence('S'),
                new Occurence('T'),
                new Occurence('U'),
                new Occurence('V'),
                new Occurence('W'),
                new Occurence('X'),
                new Occurence('Y'),
                new Occurence('Z')
            };
            foreach (char c in input)
            {
                if (!char.IsLetter(c))
                    continue;
                occurences.Where(x => x.Letter == c).First().Count++;
            }

            var map = new List<LetterToLetter>();
            var inputProbabilities = new List<Probability>();
            foreach (var occurence in occurences)
            {
                var probability = (double)occurence.Count / letterCount * 100;
                inputProbabilities.Add(new Probability(occurence.Letter, probability));
            }

            var orderedInputProbabilities = inputProbabilities.OrderBy(x => x.Chance).ToList();
            var orderedPolishProbabilities = polishProbabilities.OrderBy(x => x.Chance).ToList();

            for (int i = 0; i < orderedInputProbabilities.Count(); i++)
            {
                map.Add(new LetterToLetter(orderedInputProbabilities[i].Letter, orderedPolishProbabilities[i].Letter));
            }

            var output = string.Empty;
            foreach (char c in input)
            {
                if (!char.IsLetter(c))
                {
                    output += c;
                    continue;
                }

                output += map.Where(x => x.LetterFromInput == c).First().LetterFromPolish;
            }

            return output;
        }
    }

    class Probability
    {
        public Probability(char letter, double chance)
        {
            Letter = letter;
            Chance = chance;
        }
        public char Letter { get; set; }
        public double Chance { get; set; }
    }
    class Occurence
    {
        public Occurence(char letter, int count = 0)
        {
            Letter = letter;
            Count = count;
        }
        public char Letter { get; set; }
        public int Count { get; set; }
    }
    class LetterToLetter
    {
        public LetterToLetter(char letterFromInput, char letterFromPolish)
        {
            LetterFromInput = letterFromInput;
            LetterFromPolish = letterFromPolish;
        }
        public char LetterFromInput { get; set; }
        public char LetterFromPolish { get; set; }
    }
}
