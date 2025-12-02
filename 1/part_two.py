with open("input_one.txt") as f:
    moves = f.readlines()


# moves = ["R65", "R85", "L50", "L11", "L40", "R51", "R49", "R772", "L532"]


zero_crossings = 0
zero_landings = 0
cur = 50

for move in moves:
    direction = move[0]
    distance = int(move[1:])
    full_circles = distance // 100
    zero_crossings += full_circles
    remainder = distance - (full_circles * 100)
    if direction == "L":
        zero_crossings += 1 if (cur - remainder) < 0 and cur != 0 else 0
        next_num = (cur - remainder) % 100
        print(f"from {cur} turn LEFT {distance} to {next_num}")
        cur = next_num
    else:
        zero_crossings += 1 if (cur + remainder) > 100 else 0
        next_num = (cur + remainder) % 100
        print(f"from {cur} turn RIGHT {distance} to {next_num}")
        cur = next_num
    if cur == 0:
        zero_landings += 1
# print(cur, zero_crossings, zero_landings)
print(zero_crossings + zero_landings)
