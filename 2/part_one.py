import re
from multiprocessing import Pool, cpu_count

# puzzle_input = "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124"
puzzle_input = "67562556-67743658,62064792-62301480,4394592-4512674,3308-4582,69552998-69828126,9123-12332,1095-1358,23-48,294-400,3511416-3689352,1007333-1150296,2929221721-2929361280,309711-443410,2131524-2335082,81867-97148,9574291560-9574498524,648635477-648670391,1-18,5735-8423,58-72,538-812,698652479-698760276,727833-843820,15609927-15646018,1491-1766,53435-76187,196475-300384,852101-903928,73-97,1894-2622,58406664-58466933,6767640219-6767697605,523453-569572,7979723815-7979848548,149-216"


def check_if_str_illegal_part_one(full_num, sub_num):
    return sub_num + sub_num == full_num


def check_if_str_illegal_part_two(full_num, sub_num):
    return bool(re.search(f"^({sub_num})({sub_num})+$", full_num))


def check_if_num_illegal(num):
    string_of_num = str(num)
    idxs = range(1, len(string_of_num) + 1)
    for end_idx in idxs:
        is_illegal = check_if_str_illegal_part_two(
            string_of_num, string_of_num[0:end_idx]
        )
        if is_illegal:
            return (True, num)
    return (False, num)


nums = []
for range_ in puzzle_input.split(","):
    r = range_.split("-")
    nums += list(range(int(r[0]), int(r[1]) + 1))


illegal_nums = []
workers = cpu_count()
with Pool(workers) as pool:
    results = pool.map(check_if_num_illegal, nums)
illegal_nums = [num for (illegal, num) in results if illegal]
print(illegal_nums, sum(illegal_nums))
