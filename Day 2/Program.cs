namespace Day_2
{
    class Program
    {
        static List<Member> members = new List<Member>
        {
            new Member
            {
                FirstName = "Phuong",
                LastName = "Nguyen Nam",
                Gender = "Male",
                DateOfBirth = new DateTime(2001, 1, 22),
                PhoneNumber = "",
                BirthPlace = "Phu Tho",
                IsGraduated = false
            },
            new Member
            {
                FirstName = "Nam",
                LastName = "Nguyen Thanh",
                Gender = "Male",
                DateOfBirth = new DateTime(2001, 1, 20),
                PhoneNumber = "",
                BirthPlace = "Ha Noi",
                IsGraduated = false
            },
            new Member
            {
                FirstName = "Son",
                LastName = "Do Hong",
                Gender = "Male",
                DateOfBirth = new DateTime(2000, 11, 6),
                PhoneNumber = "",
                BirthPlace = "Ha Noi",
                IsGraduated = false
            },
            new Member
            {
                FirstName = "Huy",
                LastName = "Nguyen Duc",
                Gender = "Male",
                DateOfBirth = new DateTime(1996, 1, 26),
                PhoneNumber = "",
                BirthPlace = "Ha Noi",
                IsGraduated = false
            },
            new Member
            {
                FirstName = "Hoang",
                LastName = "Phuong Viet",
                Gender = "Male",
                DateOfBirth = new DateTime(1999, 2, 5),
                PhoneNumber = "",
                BirthPlace = "Ha Noi",
                IsGraduated = false
            },
            new Member
            {
                FirstName = "Long",
                LastName = "Lai Quoc",
                Gender = "Male",
                DateOfBirth = new DateTime(1997, 5, 30),
                PhoneNumber = "",
                BirthPlace = "Bac Giang",
                IsGraduated = false
            },
            new Member
            {
                FirstName = "Thanh",
                LastName = "Tran Chi",
                Gender = "Male",
                DateOfBirth = new DateTime(2000, 9, 18),
                PhoneNumber = "",
                BirthPlace = "Ha Noi",
                IsGraduated = false
            },
            new Member
            {
                FirstName = "Linh",
                LastName = "Nguyen",
                Gender = "Female",
                DateOfBirth = new DateTime(1996, 1, 27),
                PhoneNumber = "",
                BirthPlace = "Ha Noi",
                IsGraduated = false
            }
        };


        static void PrintData(List<Member> data)
        {
            {
                var index = 0;
                foreach (var item in data)
                {
                    ++index;
                    Console.WriteLine($"{index}. {item.LastName} {item.FirstName} - {item.DateOfBirth.ToString("dd/MM/yyyy")} - [{item.Age}]");
                }
            }
        }
        static void Main(string[] args)
        {
            //1.
            var maleMembers = GetMaleMembers();
            PrintData(maleMembers);
            // 2.
            var oldest = GetOldestMember();
            PrintData(new List<Member> { oldest });
            //3.
            var fullnames = GetFullMemberNames();
            for (int i = 0; i < fullnames.Count; i++)
            {
                string fullname = fullnames[i];
                Console.WriteLine($"{i + 1}. {fullname}");
            }
            // 4.
            var results = SplitMemberByBirthYear(2000);
            PrintData(results.Item1);
            Console.WriteLine("----------------------------------------------------");
            PrintData(results.Item2);
            Console.WriteLine("----------------------------------------------------");
            PrintData(results.Item3);
            Console.WriteLine("----------------------------------------------------");
            // 5.
            var result = GetTheFirstMemberByBirthPlace("ha noi");
            if (result != null)
                PrintData(new List<Member> { result });
            else
                Console.WriteLine("No Data!");
        }

        static List<Member> GetMaleMembers()
        {
            var results = members.Where(x => x.Gender == "Male").ToList();
            // var results = members.Select((member, index) =>
            // {
            //     if (member.Gender == "Male")
            //         return member;
            //     else
            //         return null;
            // }).Where(x => x != null).ToList();
            return results;
        }
        static Member GetOldestMember()
        {
            //    var maxAge = members.Max(m => m.Age);
            //    Console.WriteLine($"Max age: {maxAge}");
            //    return members.FirstOrDefault(m=>m.Age == maxAge);

            return members.OrderBy(m => m.TotalDays).Last();

        }
        static List<string> GetFullMemberNames()
        {
            var fullNames = members.Select(m => m.FullName);
            return fullNames.ToList();
        }
        static Tuple<List<Member>, List<Member>, List<Member>> SplitMemberByBirthYear(int year)
        {
            var list1 = members.Where(m => m.DateOfBirth.Year == year).ToList();
            var list2 = members.Where(m => m.DateOfBirth.Year > year).ToList();
            var list3 = members.Where(m => m.DateOfBirth.Year < year).ToList();

            return Tuple.Create(list1, list2, list3);
        }
        static Member GetTheFirstMemberByBirthPlace(string place)
        {
            return members.FirstOrDefault(m => m.BirthPlace.Equals(place, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
