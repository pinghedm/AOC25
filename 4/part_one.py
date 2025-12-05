# puzzle_input_raw = """
# ..@@.@@@@.
# @@@.@.@.@@
# @@@@@.@.@@
# @.@@@@..@.
# @@.@@@@.@@
# .@@@@@@@.@
# .@.@.@.@@@
# @.@@@.@@@@
# .@@@@@@@@.
# @.@.@@@.@.
# """

# puzzle_input = [l for l in puzzle_input_raw.split("\n") if l]

with open("puzzle_input.txt") as f:
    puzzle_input = f.readlines()


def find_removable_paper_coords(puzzle_input):
    coords_of_paper = set()
    coords_of_removable_paper = set()
    for y, row in enumerate(puzzle_input):
        for x, char in enumerate(row):
            if char == "@":
                coords_of_paper.add((x, y))
    for y, row in enumerate(puzzle_input):
        for x, char in enumerate(row):
            if char != "@":
                continue
            surrounding_coords = set(
                [
                    (x - 1, y - 1),
                    (x, y - 1),
                    (x + 1, y - 1),
                    (x - 1, y),
                    (x + 1, y),
                    (x - 1, y + 1),
                    (x, y + 1),
                    (x + 1, y + 1),
                ]
            )
            num_surrounding_paper = len(surrounding_coords & coords_of_paper)
            if num_surrounding_paper < 4:
                coords_of_removable_paper.add((x, y))
    return coords_of_removable_paper


# part 1
# removable_paper = find_removable_paper_coords(puzzle_input)
# print(f"num valid papers: {len(removable_paper)}")


# part 2
removable_paper = find_removable_paper_coords(puzzle_input)
total_paper_removed = len(removable_paper)
removable_paper_this_round = set("seed")
while len(removable_paper_this_round) > 0:
    new_input = [*puzzle_input]
    for x, y in removable_paper:
        row = new_input[y]
        exploded_row = [x for x in row]
        exploded_row[x] = "."
        new_input[y] = "".join(exploded_row)
    removable_paper_this_round = find_removable_paper_coords(new_input)
    removable_paper |= removable_paper_this_round
    total_paper_removed += len(removable_paper_this_round)

print(f"{total_paper_removed=}")
