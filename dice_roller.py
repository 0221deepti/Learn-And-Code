import random

def roll_dice(number_of_sides):
    return random.randint(1, number_of_sides)

def main():
    number_of_sides = 6
    keep_rolling = True

    while keep_rolling:
        user_input = input("Ready to roll? Enter Q to quit: ")

        if user_input.lower() != "q":
            rolled_number = roll_dice(number_of_sides)
            print(f"You have rolled a {rolled_number}")
        else:
            keep_rolling = False


if __name__ == "__main__":
    main()
