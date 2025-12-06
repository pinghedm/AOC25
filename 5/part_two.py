import functools

# puzzle_input = """3-5
# 10-14
# 16-20
# 12-18
# """
with open("input_two.txt") as f:
    puzzle_input = f.read()


def parseToRange(r):
    start, end = r.split("-")
    return (int(start), int(end))


ranges = [parseToRange(r) for r in puzzle_input.split("\n")[:-1]]
sorted_ranges = sorted(ranges)


merged_ranges = [sorted_ranges[0]]
for start, end in sorted_ranges[1:]:
    highest_start = merged_ranges[-1][0]
    highest_end = merged_ranges[-1][1]

    if start >= highest_start and start <= highest_end:
        if end <= highest_end:
            continue
        else:
            merged_ranges[-1] = (highest_start, end)
    else:
        merged_ranges.append((start, end))

total_nums = 0
for r in merged_ranges:
    diff = int(r[1]) - int(r[0]) + 1
    total_nums += diff


print(f"{total_nums=}")
