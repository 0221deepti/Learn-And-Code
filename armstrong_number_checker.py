def calculate_armstrong_sum(number):
    armstrong_sum = 0
    number_of_digits = len(str(number))
    remaining_number = number

    while remaining_number > 0:
        digit = remaining_number % 10
        armstrong_sum += digit ** number_of_digits
        remaining_number //= 10

    return armstrong_sum


def main():
    user_number = int(input("Please enter a number to check if it is an Armstrong number: "))

    if user_number == calculate_armstrong_sum(user_number):
        print(f"{user_number} is an Armstrong number.")
    else:
        print(f"{user_number} is NOT an Armstrong number.")


if __name__ == "__main__":
    main()
