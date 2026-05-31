secret_letter = 'в'
print("Привет! Я угадал букву от а до м. Попробуй угадать!")
while True:
    guess = input("Введи букву: ")
    if guess == secret_letter:
        print("Поздравляю! Ты угадал букву!")
        break
    else:
        print("Неправильно! Попробуй снова.")