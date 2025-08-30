using System;
using System.IO;
using System.Numerics;

namespace Fermat_sLastTheorem
{
    class Program
    {
        public static readonly string ResourcesDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\Fermat_sLastTheorem\";
        public static readonly string TempDir = Path.GetTempPath() + @"\";

        private static void Main()
        {
            //乗数
            Console.WriteLine("フェルマーの最終定理に挑め！乗数を入力しよう！");
            var n = 0;
            var inputFlg = true;

            do
            {
                var nStr = Console.ReadLine();

                try
                {
                    n = int.Parse(nStr);
                    inputFlg = false;
                }
                catch (FormatException)
                {
                    Console.WriteLine("数値以外が入力されたよ！");
                }
            }
            while (inputFlg);

            var xTxt = ResourcesDir + "x" + n.ToString();
            var yTxt = ResourcesDir + "y" + n.ToString();
            //var zTxt = TempDir + "z" + n.ToString();
            var answerTxt = ResourcesDir + "a" + n.ToString();

            var x = new BigInteger(1);
            var y = new BigInteger(1);
            var z = new BigInteger(1);

            try
            {
                x = BigInteger.Parse(File.ReadAllText(xTxt));
                y = BigInteger.Parse(File.ReadAllText(yTxt));
                // z = BigInteger.Parse(File.ReadAllText(zTxt));
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(ResourcesDir);
            }
            catch (FileNotFoundException)
            {
                File.WriteAllText(xTxt, x.ToString());
                File.WriteAllText(yTxt, y.ToString());
                // File.WriteAllText(zTxt, z.ToString());
            }
            catch (FormatException)
            {
                File.WriteAllText(xTxt, x.ToString());
                File.WriteAllText(yTxt, y.ToString());
                // File.WriteAllText(zTxt, z.ToString());
            }

            Console.WriteLine($"n={n}で計算");

            // フェルマーの最終定理を数値計算

            while (true)
            {
                var xx = BigInteger.Pow(x, n);

                while (y <= x)
                {
                    var yy = BigInteger.Pow(y, n);

                    while (z < x + y)
                    {
                        var zz = BigInteger.Pow(z, n);

                        if (xx + yy == zz)
                        {
                            var answer = $"x={x}  y={y}  z={z}  {xx}+{yy}={zz}" + Environment.NewLine;
                            Console.Write(answer);
                            File.AppendAllText(answerTxt, answer);
                        }

                        ++z;
                        //File.WriteAllText(zTxt, z.ToString());
                    }

                    z = new BigInteger(1);
                    ++y;

                    if (y % 20000 == 0)
                    {
                        File.WriteAllText(yTxt, y.ToString());
                    }
                }

                y = new BigInteger(1);
                ++x;
                File.WriteAllText(xTxt, x.ToString());
            }
        }
    }
}
