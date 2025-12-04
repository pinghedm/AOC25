# I don't quite understand this solution, needed help from reddit


def max_num(line, num_batteries_needed):
    joltage = 0
    remaining_batteries = num_batteries_needed
    start_idx = 0
    digits = [int(x) for x in line]
    selected_digits = []
    # print("for ", line)
    while remaining_batteries > 0:
        # print(
        #     f"{remaining_batteries=} {selected_digits=} {digits[start_idx : len(digits) - remaining_batteries + 1]}"
        # )
        idx = 0
        max_digit = -1
        for i, d in enumerate(
            digits[start_idx : len(digits) - remaining_batteries + 1]
        ):
            if d > max_digit:
                idx = i
                max_digit = d
        selected_digits.append(max_digit)
        remaining_batteries -= 1
        start_idx = start_idx + idx + 1
    return selected_digits


import functools


def list_to_int(l):
    return functools.reduce(lambda acc, next: acc * 10 + next, l)


# input_banks = """
# 987654321111111
# 811111111111119
# 234234234234278
# 818181911112111
# """

# input_banks = "1111911"

with open("puzzle_input_one.txt") as f:
    input_banks = f.read()

vals = []
for l in [l_ for l_ in input_banks.split("\n") if l_]:
    v = max_num(l, 12)
    vals.append(list_to_int(v))
    print(l, max_num(l, 12))
print(f"{sum(vals)=}")
