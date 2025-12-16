# 1. The below program is to Roll the Dice
import random
def rollDice(sidesOfDice):
    numberAppearedOnDice = random.randint(1, sidesOfDice)
    return numberAppearedOnDice

def main():
    sidesOfDice = 6
    keepRollingTheDice = True
    while keepRollingTheDice:
        userInput = input("Ready to roll? Enter Q to Quit: ")
        if userInput.lower() != "q":
            numberAppearedOnDice = rollDice(sidesOfDice)
            print("You have rolled a",numberAppearedOnDice)
        else:
            keepRollingTheDice = False

# 2: The below program is to guess the correct number between 1 to 100
def isValidInput(userInput):
    if userInput.isdigit() and 1<= int(userInput) <=100:
        return True
    else:
        return False

def main():
    numberToGuess = random.randint(1,100)
    isGuessedCorrectly = False
    totalNumberOfGuess = 0

    userInput = input("Guess a number between 1 and 100:")
    while not isGuessedCorrectly:
        if not isValidInput(userInput):
            userInput = input("I wont count this one Please enter a number between 1 to 100")
            continue
        else:
            totalNumberOfGuess += 1
            userInput = int(userInput)

        if userInput < numberToGuess:
            userInput = input("Too low. Guess again")
        elif userInput > numberToGuess:
            userInput = input("Too High. Guess again")
        else:
            print("You guessed it in",totalNumberOfGuess,"guesses!")
            isGuessedCorrectly = True

main()

# 3: The below program is to check whether the number is Armstrong number or not

def isArmstrongNumber(userInput):
    # Initializing Sum and Number of Digits
    sumOfPowers = 0
    numberOfDigits = 0

    # Calculating Number of individual digits
    tempNumber = userInput
    while tempNumber > 0:
        numberOfDigits = numberOfDigits + 1
        tempNumber = tempNumber // 10

    # Finding Armstrong Number
    tempNumber = userInput
    for n in range(1, tempNumber + 1):
        remainder = tempNumber % 10
        sumOfPowers = sumOfPowers + (remainder ** numberOfDigits)
        tempNumber //= 10
    return sumOfPowers


# End of Function

# User Input
userInput = int(input("\nPlease Enter the Number to Check for Armstrong: "))

if (userInput == isArmstrongNumber(userInput)):
    print("\n %d is Armstrong Number.\n" % userInput)
else:
    print("\n %d is Not a Armstrong Number.\n" % userInput)
 
