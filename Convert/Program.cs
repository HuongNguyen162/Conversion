using System;
using System.Collections.Generic;

namespace Test
{
    public class Conversion
    {
        int a, b;
        string s;
        public void input()
        {
            Console.WriteLine("\nNhap he so trong he a : ");
            a = int.Parse(Console.ReadLine());
            while (a < 2 && a > 16)
            {
                Console.WriteLine("Nhap lai he so a (>=2 hoac <=16): ");
                a = int.Parse(Console.ReadLine());
            }

            Console.WriteLine("Nhap he so muon chuyen doi sang b: ");
            b = int.Parse(Console.ReadLine());
            while (a < 2 && a > 16)
            {
                Console.WriteLine("Nhap lai he so b (>=2 hoac <=16): ");
                a = int.Parse(Console.ReadLine());
            }

            Console.WriteLine("Nhap chuoi can chuyen doi: ");
            s = Console.ReadLine();
        }

        public static int getSplitStringLength(string inputString, string[] delimiter) // dem so lan ptu recur2 lap lai bnh lan
        {
            string[] splitString = inputString.Split(delimiter, StringSplitOptions.None);
            return splitString.Length - 1; //vd: 1100 1100 1100 => 1100 -> 4
        }

        public static string recurring(string recur) //dua ra phan tu chung nhat
        {
            string[] recur2 = new string[1];
            recur2[0] = "";

            for (int i = 0; i < recur.Length; i++)
            {
                recur2[0] += recur.Substring(i, 1); //ham cat phan tu --> chuoi cat duoc, xet xem co trung lap trong recur khong
                if (recur.Length == getSplitStringLength(recur, recur2) * recur2[0].Length) //kiem tra xem dung phan tu bi cat lap lai trong mang khong: so lan lap x chieu dai chuoi lap
                {
                    break;
                }
            }
            return recur2[0];
        }


        // Function to return ASCII value of a character (chuyển các ký tự chữ sang bảng mã --> tính toán)
        static int val(char c)
        {
            if (c >= '0' && c <= '9')
                return (int)c - '0'; // ep kieu sang kieu int
            else
                return (int)c - 'A' + 10;
        }

        // Function to return equivalent character of a given value
        static char reVal(int num) //Chuyển ngược lại các ký tự trong bảng mã --> ký tự chữ --> xuất ra màn hình
        {
            if (num >= 0 && num <= 9)
                return (char)(num + '0');
            else
                return (char)(num - 10 + 'A');
        }

        // Function to convert a number from given basse to decimal number
        static int toDeci(string str, int basse)
        {
            // Stores the length of the string
            int len = str.Length;

            // Initialize power of basse
            int power = 1; // chinh là Basse mũ 0 = 1

            // Initialize result
            int num = 0;

            // Decimal equivalent is str[len-1]*1 + str[len-2]*basse + str[len-3]*(basse^2) + ...
            for (int i = len - 1; i >= 0; i--)
            {
                // A digit in input number must be less than number's basse
                if (val(str[i]) >= basse) // kiem tra dieu kieen day nhap vao. VD: basse=2 --> chi được nhập 0,1
                {
                    Console.Write("Invalid Number");
                    return -1;
                }

                // Update num
                num += val(str[i]) * power; //VD: 13 x (16^2)

                // Update power
                power = power * basse; //Số mũ của cơ số cần đổi ra cơ số 10 tăng dần. vd: (16^2) x 16 = 16^3
            }
            return num;
        }

        // Function to convert binary fractional to decimal (chuyển phần lẻ các basse --> 10)
        static double ToDecimalComma(string binary, int basse)
        {
            for (int i = binary.Length - 1; i >= 0; i--)
            {
                // A digit in input number must be less than number's basse
                if (binary[i] != '.' && val(binary[i]) >= basse) // kiem tra dieu kieen day nhap vao. VD: basse=2 --> chi được nhập 0,1
                {
                    Console.Write("Invalid Number");
                    return -1;
                }
            }

            double intDecimal = 0, fracDecimal = 0, power = 1; //power: số mũ, mũ 0 =1

            // Fetch the radix point
            int point = binary.IndexOf('.'); //Tìm dấu phẩy

            // Update point if not found
            if (point == -1)
            {
                // Nếu không tìm thấy ký tự sẽ gắn bằng chiều dài để tính bth
                point = binary.Length;
            }

            // Convert integral part of binary to decimal equivalent
            for (int i = point - 1; i >= 0; i--)
            {
                intDecimal += val(binary[i]) * power;
                power *= basse;
            }

            if (point != binary.Length)
            {
                // Convert fractional part of binary to decimal equivalent
                power = basse; //Găn lại vì ở trên đã tính toán
                for (int i = point + 1; i < binary.Length; i++)
                {
                    fracDecimal += val(binary[i]) / power; // bc la nhan voi mu -1,...
                    power *= basse;
                }
            }

            // Add both integral and fractional part
            return intDecimal + fracDecimal;
        }

        // Function to convert a given decimal number to a given basse
        static string fromDeci(int basse, int inputNum)
        {

            // Store the result
            string res = "";

            // Repeatedly divide inputNum by basse and take remainder
            while (inputNum > 0) // dieu kien chia tiep laf so bi chia lon hon 0
            {

                // Update res
                res += reVal(inputNum % basse); //lấy phần dư --> đổi sang ký tự

                // Update inputNum
                inputNum /= basse; //update lại số bị chia. vd: 7/2= 3(input number)
            }

            // Reverse the result
            res = reverse(res); // đảo ngược kết quả chia để được số đổi từ hệ 10 sang hệ khác

            return res;
        }

        // Function to convert decimal to binary fractional (chuyển phần lẻ basse 10 --> các basse )
        static String fromDeciComma(int basse, double num)
        {
            int k_prec = 12; //độ chính xác
            String binary = ""; // lưu trữ kết quả cuối cùng

            //// Fetch the integral part of decimal number. Tach 2 phan: truoc dau phay va sau dau phay
            int Integral = (int)num; // phan nguyen

            // Fetch the fractional part decimal number. 
            double fractional = num - Integral; // phan du


            ////Doi phan truoc dau phay. Doi phan tu he 10 sang he khac
            binary += fromDeci(basse, Integral);

            string fractionalPart = "";
                
            //// Conversion of fractional part to binary equivalent. Doi phan sau dau phay
            while (k_prec-- > 0)
            {
                // Find next bit in fraction
                fractional *= basse; // Nhan 
                int fract_bit = (int)fractional; // Lay phan nguyen

                fractional -= fract_bit; // Lay ket qua nhan duoc tru nguyen de lay phan le
                fractionalPart += reVal(fract_bit); // ghep lai
            }

            // Append point before conversion of fractional part
            string recurredFractionalPart = recurring(fractionalPart);

            if (recurredFractionalPart != fractionalPart)
            {
                recurredFractionalPart = "(" + recurredFractionalPart + ")";
            }

            binary += ('.') + recurredFractionalPart;

            return binary;
        }

        /*
        static int toDeciComma(string str, int basse)
        {
            int k_prec = 3;

            double floatInput = Convert.ToDouble(str);

            // Fetch the integral part of decimal number
            int Integral = (int)floatInput;
            string IntegralString = Integral.ToString();

            // Fetch the fractional part decimal number
            double fractional = floatInput - Integral;

            // Stores the length of the string
            int len = IntegralString.Length;

            // Initialize power of basse
            int power = 1; // chinh là Basse mũ 0 = 1

            // Initialize result
            int num = 0;

            // Decimal equivalent is str[len-1]*1 + str[len-2]*basse + str[len-3]*(basse^2) + ...
            for (int i = len - 1; i >= 0; i--)
            {
                // A digit in input number must be less than number's basse
                if (val(IntegralString[i]) >= basse) // kiem tra dieu kieen day nhap vao. VD: basse=2 --> chi được nhập 0,1
                {
                    Console.Write("Invalid Number");
                    return -1;
                }

                // Update num
                num += val(IntegralString[i]) * power; //VD: 13 x (16^2)

                // Update power
                power = power * basse; //Số mũ của cơ số cần đổi ra cơ số 10 tăng dần. vd: (16^2) x 16 = 16^3
            }


            while (k_prec-- > 0) // (k_prec = k_prec - 1) > 0
            {
                // Find next bit in fraction
                fractional *= basse;
                int fract_bit = (int)fractional;

                if (fract_bit > 0)
                {
                    fractional -= fract_bit;
                    num += (char)(fract_bit + '0');
                }
                else
                {
                    num += (char)(0 + '0');
                }
            }

            return num;
        }
        */

        // Function to convert a given number from a basse to another basse (chuyển các basse bất kỳ, dùng 10 làm bắc cầu)
        static void convertbasse(string s, int a, int b) // a, b là 2 he so --> chuyển đổi từ hệ a sang b
        {

            // Convert the number from basse A to decimal
            int num = toDeci(s, a); // chuyển đổi tử  hệ khác sang 10, num là bắc cầu

            // Convert the number from decimal to basse B
            string ans = fromDeci(b, num); // chuyển tử hệ 10 sang hệ khác

            // Print the result
            Console.Write(ans); //kết quả cuối cùng
        }

        static void convertCommmabasse(string s, int a, int b) // a, b là 2 he so --> chuyển đổi từ hệ a sang b
        {

            // Convert the number from basse A to decimal
            double num = ToDecimalComma(s, a); // chuyển đổi tử  hệ khác sang 10, num là bắc cầu
            
            if (num > -1)
            {
                // Convert the number from decimal to basse B
                string ans = fromDeciComma(b, num); // chuyển tử hệ 10 sang hệ khác

                // Print the result
                Console.Write(ans); //kết quả cuối cùng
            }
        }

        static string reverse(string input) //Đổi chỗ ngược lại. Vì sau khi đổi từ hệ khác ra hệ 10 (phép chia) thì cần đọc ngược lại kết quả
        {
            char[] a = input.ToCharArray(); // đưa vào mảng để đổi chỗ
            int l, r = a.Length - 1; //r là số cuối cùng trong mảng
            for (l = 0; l < r; l++, r--) //đổi chỗ
            {
                char temp = a[l];
                a[l] = a[r];
                a[r] = temp;
            }
            return new string(a); //hop ca phan tu trong mang lai
        }

        static public void Main()
        {
            var m = new Conversion();
            int select;

            do
            {
                Console.WriteLine("\n\n*********************************MENU**********************************");
                Console.WriteLine("01.Doi he so");
                Console.WriteLine("02.Thoát chương trình");
                Console.WriteLine("********************NHAP LUA CHON CUA BAN VAO MAN HINH*********************");
                Console.WriteLine("Lua chon cua ban la: ");
                select = int.Parse(Console.ReadLine());
                switch (select)
                {
                    case 1:
                        m.input();
                        if (m.s.IndexOf(".") < 0)
                        {
                            convertbasse(m.s, m.a, m.b);
                        } else
                        {
                            convertCommmabasse(m.s, m.a, m.b);
                        }
                        break;

                    case 2:
                        Console.WriteLine("\nTam biet nhe. Ket thuc chuong trinh!");
                        break;
                }
            } while (select != 2);


        }
    }
}
