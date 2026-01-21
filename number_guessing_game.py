import random


def is_valid_guess(user_input):
    return user_input.isdigit() and 1 <= int(user_input) <= 100


def main():
    target_number = random.randint(1, 100)
    has_guessed_correctly = False
    guess_count = 0

    user_input = input("Guess a number between 1 and 100: ")

    while not has_guessed_correctly:
        if not is_valid_guess(user_input):
            user_input = input(
                "Invalid input. Please enter a number between 1 and 100: "
            )
            continue

        guess_count += 1
        guessed_number = int(user_input)

        if guessed_number < target_number:
            user_input = input("Too low. Guess again: ")
        elif guessed_number > target_number:
            user_input = input("Too high. Guess again: ")
        else:
            print(f"You guessed it in {guess_count} guesses!")
            has_guessed_correctly = True


if __name__ == "__main__":
    main()
