using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilApp.Class
{
    class ItemMath
    {
        public static Item Multiply(Item item1, Item item2)
        {

            int decimalPlace = 2;

            string string1 = MathHelper.RoundDec(item1.number, decimalPlace) + item1.unit;
            string string2 = MathHelper.RoundDec(item2.number, decimalPlace) + item2.unit;

            string eq = string1 + " x " + string2;
            double ans = item1.number * item2.number;

            Item newItem = new Class.Item("Multiplication", ans, "", eq);

            return newItem;
        }

        public static Item Add(Item item1, Item item2)
        {
            int decimalPlace = 2;

            string string1 = MathHelper.RoundDec(item1.number, decimalPlace) + item1.unit;
            string string2 = MathHelper.RoundDec(item2.number, decimalPlace) + item2.unit;

            string eq = string1 + " + " + string2;
            double ans = item1.number + item2.number;

            Item newItem = new Class.Item("Addition", ans, "", eq);

            return newItem;
        }

        public static Item Subtract(Item item1, Item item2)
        {
            int decimalPlace = 2;

            string string1 = MathHelper.RoundDec(item1.number, decimalPlace) + item1.unit;
            string string2 = MathHelper.RoundDec(item2.number, decimalPlace) + item2.unit;

            string eq = string1 + " - " + string2;
            double ans = item1.number - item2.number;

            Item newItem = new Class.Item("Subtraction", ans, "", eq);

            return newItem;
        }
        public static Item Divide(Item item1, Item item2)
        {
            int decimalPlace = 2;

            string string1 = MathHelper.RoundDec(item1.number, decimalPlace) + item1.unit;
            string string2 = MathHelper.RoundDec(item2.number, decimalPlace) + item2.unit;

            string eq = string1 + " / " + string2;
            double ans = item1.number / item2.number;

            Item newItem = new Class.Item("Division", ans, "", eq);

            return newItem;
        }

        public static Item AddMultiply(Item item1, Item item2, Item item3)//item1 + item2 * item3
        {
            int decimalPlace = 2;

            string string1 = MathHelper.RoundDec(item1.number, decimalPlace) + item1.unit;
            string string2 = MathHelper.RoundDec(item2.number, decimalPlace) + item2.unit;
            string string3 = MathHelper.RoundDec(item3.number, decimalPlace) + item3.unit;

            string eq = string1 + " + " + string2 + " x " + string3;
            double ans = item1.number + item2.number * item3.number;

            Item newItem = new Class.Item("Addition Multiplication", ans, "", eq);

            return newItem;
        }

        public static Item AddSubtract(Item item1, Item item2, Item item3)//item1 + item2 - item3
        {
            int decimalPlace = 2;

            string string1 = MathHelper.RoundDec(item1.number, decimalPlace) + item1.unit;
            string string2 = MathHelper.RoundDec(item2.number, decimalPlace) + item2.unit;
            string string3 = MathHelper.RoundDec(item3.number, decimalPlace) + item3.unit;

            string eq = string1 + " + " + string2 + " - " + string3;
            double ans = item1.number + item2.number - item3.number;

            Item newItem = new Class.Item("Addition Subtraction", ans, "", eq);

            return newItem;
        }

        public static Item SubtractAdd(Item item1, Item item2, Item item3)//item1 - item2 + item3
        {
            int decimalPlace = 2;

            string string1 = MathHelper.RoundDec(item1.number, decimalPlace) + item1.unit;
            string string2 = MathHelper.RoundDec(item2.number, decimalPlace) + item2.unit;
            string string3 = MathHelper.RoundDec(item3.number, decimalPlace) + item3.unit;

            string eq = string1 + " - " + string2 + " + " + string3;
            double ans = item1.number - item2.number + item3.number;

            Item newItem = new Class.Item("Subtraction Addition", ans, "", eq);

            return newItem;
        }

        public static Item MultiplyAdd(Item item1, Item item2, Item item3)//item1 + item2 * item3
        {
            int decimalPlace = 2;

            string string1 = MathHelper.RoundDec(item1.number, decimalPlace) + item1.unit;
            string string2 = MathHelper.RoundDec(item2.number, decimalPlace) + item2.unit;
            string string3 = MathHelper.RoundDec(item3.number, decimalPlace) + item3.unit;

            string eq = string1 + " x " + string2 + " + " + string3;
            double ans = item1.number * item2.number + item3.number;

            Item newItem = new Class.Item("Multiplication Addition", ans, "", eq);

            return newItem;
        }

        public static Item SubtractMultiply(Item item1, Item item2, Item item3)//item1 - item2 * item3
        {
            int decimalPlace = 2;

            string string1 = MathHelper.RoundDec(item1.number, decimalPlace) + item1.unit;
            string string2 = MathHelper.RoundDec(item2.number, decimalPlace) + item2.unit;
            string string3 = MathHelper.RoundDec(item3.number, decimalPlace) + item3.unit;

            string eq = string1 + " - " + string2 + " x " + string3;
            double ans = item1.number - item2.number * item3.number;

            Item newItem = new Class.Item("Subtraction Multiplication", ans, "", eq);

            return newItem;
        }

        public static Item SubtractDivide(Item item1, Item item2, Item item3)//item1 - item2 * item3
        {
            int decimalPlace = 2;

            string string1 = MathHelper.RoundDec(item1.number, decimalPlace) + item1.unit;
            string string2 = MathHelper.RoundDec(item2.number, decimalPlace) + item2.unit;
            string string3 = MathHelper.RoundDec(item3.number, decimalPlace) + item3.unit;

            string eq = string1 + " - " + string2 + " / " + string3;
            double ans = item1.number - item2.number / item3.number;

            Item newItem = new Class.Item("Subtraction Division", ans, "", eq);

            return newItem;
        }

        public static Item DivideSubtract(Item item1, Item item2, Item item3)//item1 / item2 - item3
        {
            int decimalPlace = 2;

            string string1 = MathHelper.RoundDec(item1.number, decimalPlace) + item1.unit;
            string string2 = MathHelper.RoundDec(item2.number, decimalPlace) + item2.unit;
            string string3 = MathHelper.RoundDec(item3.number, decimalPlace) + item3.unit;

            string eq = string1 + " / " + string2 + " - " + string3;
            double ans = item1.number / item2.number - item3.number;

            Item newItem = new Class.Item("Division Subtraction", ans, "", eq);

            return newItem;
        }

        public static Item MultiplyDivide(Item item1, Item item2, Item item3)
        {
            int decimalPlace = 2;

            string string1 = MathHelper.RoundDec(item1.number, decimalPlace) + item1.unit;
            string string2 = MathHelper.RoundDec(item2.number, decimalPlace) + item2.unit;
            string string3 = MathHelper.RoundDec(item3.number, decimalPlace) + item3.unit;

            string eq = string1 + " x " + string2 + " / " + string3;
            double ans = item1.number * item2.number / item3.number;

            Item newItem = new Class.Item("Multiplication Division", ans, "", eq);

            return newItem;
        }

        public static Item MultiplyMultiply(Item item1, Item item2, Item item3)
        {
            int decimalPlace = 2;

            string string1 = MathHelper.RoundDec(item1.number, decimalPlace) + item1.unit;
            string string2 = MathHelper.RoundDec(item2.number, decimalPlace) + item2.unit;
            string string3 = MathHelper.RoundDec(item3.number, decimalPlace) + item3.unit;

            string eq = string1 + " x " + string2 + " x " + string3;
            double ans = item1.number * item2.number * item3.number;

            Item newItem = new Class.Item("Multiplication Multiplication", ans, "", eq);

            return newItem;
        }

        public static Item MultiplyMultiplyMultiply(Item item1, Item item2, Item item3, Item item4)
        {
            int decimalPlace = 2;

            string string1 = MathHelper.RoundDec(item1.number, decimalPlace) + item1.unit;
            string string2 = MathHelper.RoundDec(item2.number, decimalPlace) + item2.unit;
            string string3 = MathHelper.RoundDec(item3.number, decimalPlace) + item3.unit;
            string string4 = MathHelper.RoundDec(item4.number, decimalPlace) + item4.unit;

            string eq = string1 + " x " + string2 + " x " + string3 + " x " + string4;
            double ans = item1.number * item2.number * item3.number * item4.number;

            Item newItem = new Class.Item("Multiplication Multiplication Multiplication", ans, "", eq);

            return newItem;
        }

        public static Item MultiplyMultiplyDivide(Item item1, Item item2, Item item3, Item item4)
        {
            int decimalPlace = 2;

            string string1 = MathHelper.RoundDec(item1.number, decimalPlace) + item1.unit;
            string string2 = MathHelper.RoundDec(item2.number, decimalPlace) + item2.unit;
            string string3 = MathHelper.RoundDec(item3.number, decimalPlace) + item3.unit;
            string string4 = MathHelper.RoundDec(item4.number, decimalPlace) + item4.unit;

            string eq = string1 + " x " + string2 + " x " + string3 + " / " + string4;
            double ans = item1.number * item2.number * item3.number / item4.number;

            Item newItem = new Class.Item("Multiplication Multiplication Division", ans, "", eq);

            return newItem;
        }

        public static Item MultiplySubtractMultiply(Item item1, Item item2, Item item3, Item item4)
        {
            int decimalPlace = 2;

            string string1 = MathHelper.RoundDec(item1.number, decimalPlace) + item1.unit;
            string string2 = MathHelper.RoundDec(item2.number, decimalPlace) + item2.unit;
            string string3 = MathHelper.RoundDec(item3.number, decimalPlace) + item3.unit;
            string string4 = MathHelper.RoundDec(item4.number, decimalPlace) + item4.unit;

            string eq = string1 + " x " + string2 + " - " + string3 + " x " + string4;
            double ans = item1.number * item2.number - item3.number * item4.number;

            Item newItem = new Class.Item("Multiplication Subtraction Multiplication", ans, "", eq);

            return newItem;
        }

        public static Item MultiplyEquation(Item item1, Item item2)
        {

            int decimalPlace = 2;

            string string1 = MathHelper.RoundDec(item1.number, decimalPlace) + item1.unit;

            string eq = string1 + " x (" + item2.note + ")";
            double ans = item1.number * item2.number;

            Item newItem = new Class.Item("Multiplication(Equation)", ans, "", eq);

            return newItem;
        }

        public static Item MultiplyEquationNoBracket(Item item1, Item item2)
        {

            int decimalPlace = 2;

            string string1 = MathHelper.RoundDec(item1.number, decimalPlace) + item1.unit;

            string eq = string1 + " x " + item2.note;
            double ans = item1.number * item2.number;

            Item newItem = new Class.Item("Multiplication Equation", ans, "", eq);

            return newItem;
        }

        public static Item EquationMultiply(Item item1, Item item2)
        {

            int decimalPlace = 2;

            string string2 = MathHelper.RoundDec(item2.number, decimalPlace) + item2.unit;

            string eq = "(" + item1.note + ") x " + string2;
            double ans = item1.number * item2.number;

            Item newItem = new Class.Item("(Equation)Multiplication", ans, "", eq);

            return newItem;
        }

        public static Item EquationMultiplyNoBracket(Item item1, Item item2)
        {

            int decimalPlace = 2;

            string string2 = MathHelper.RoundDec(item2.number, decimalPlace) + item2.unit;

            string eq = "" + item1.note + " x " + string2;
            double ans = item1.number * item2.number;

            Item newItem = new Class.Item("Equation Multiplication", ans, "", eq);

            return newItem;
        }

        public static Item DivideEquation(Item item1, Item item2)
        {

            int decimalPlace = 2;

            string string1 = MathHelper.RoundDec(item1.number, decimalPlace) + item1.unit;
            string string2 = MathHelper.RoundDec(item2.number, decimalPlace) + item2.unit;

            string eq = string1 + " / (" + item2.note + ")";
            double ans = item1.number / item2.number;

            Item newItem = new Class.Item("Division(Equation)", ans, "", eq);

            return newItem;
        }

        public static Item EquationDivideNoBracket(Item item1, Item item2)
        {

            int decimalPlace = 2;

            string string2 = MathHelper.RoundDec(item2.number, decimalPlace) + item2.unit;

            string eq = item1.note + " / " + string2;
            double ans = item1.number / item2.number;

            Item newItem = new Class.Item("Equation / ", ans, "", eq);
            return newItem;
        }


        public static Item EquationDivideEquation(Item item1, Item item2)
        {

            int decimalPlace = 2;

            string string1 = MathHelper.RoundDec(item1.number, decimalPlace) + item1.unit;
            string string2 = MathHelper.RoundDec(item2.number, decimalPlace) + item2.unit;

            string eq = "(" + item1.note + ") / (" + item2.note + ")";
            double ans = item1.number / item2.number;

            Item newItem = new Class.Item("(Equation)/(Equation)", ans, "", eq);

            return newItem;
        }

        public static Item EquationDivideEquationNoBracket(Item item1, Item item2)
        {

            int decimalPlace = 2;

            string string1 = MathHelper.RoundDec(item1.number, decimalPlace) + item1.unit;
            string string2 = MathHelper.RoundDec(item2.number, decimalPlace) + item2.unit;

            string eq = item1.note + " / " + item2.note;
            double ans = item1.number / item2.number;

            Item newItem = new Class.Item("Equation/Equation", ans, "", eq);

            return newItem;
        }

        public static Item EquationAddEquationNoBracket(Item item1, Item item2)
        {

            int decimalPlace = 2;

            string string1 = MathHelper.RoundDec(item1.number, decimalPlace) + item1.unit;
            string string2 = MathHelper.RoundDec(item2.number, decimalPlace) + item2.unit;

            string eq = item1.note + " + " + item2.note;
            double ans = item1.number + item2.number;

            Item newItem = new Class.Item("Equation + Equation", ans, "", eq);

            return newItem;
        }

        public static Item EquationMultiplyEquation(Item item1, Item item2)
        {

            int decimalPlace = 2;

            string string1 = MathHelper.RoundDec(item1.number, decimalPlace) + item1.unit;
            string string2 = MathHelper.RoundDec(item2.number, decimalPlace) + item2.unit;

            string eq = "(" + item1.note + ") x (" + item2.note + ")";
            double ans = item1.number * item2.number;

            Item newItem = new Class.Item("(Equation) x (Equation)", ans, "", eq);

            return newItem;
        }

        public static Item EquationMultiplyEquationNoBracket(Item item1, Item item2)
        {

            int decimalPlace = 2;

            string string1 = MathHelper.RoundDec(item1.number, decimalPlace) + item1.unit;
            string string2 = MathHelper.RoundDec(item2.number, decimalPlace) + item2.unit;

            string eq = item1.note + " x " + item2.note;
            double ans = item1.number * item2.number;

            Item newItem = new Class.Item("Equation x Equation", ans, "", eq);

            return newItem;
        }

        public static Item EquationDivide(Item item1, Item item2)
        {

            int decimalPlace = 2;

            string string2 = MathHelper.RoundDec(item2.number, decimalPlace) + item2.unit;

            string eq = "(" + item1.note + ") / " + string2;
            double ans = item1.number / item2.number;

            Item newItem = new Class.Item("(Equation)Division", ans, "", eq);

            return newItem;
        }


        public static Item Square(Item item1)
        {

            int decimalPlace = 2;

            string string1 = MathHelper.RoundDec(item1.number, decimalPlace) + item1.unit;

            string eq = string1 + "²";
            double ans = item1.number * item1.number;

            Item newItem = new Class.Item("Square", ans, "", eq);

            return newItem;
        }

        public static Item SquareWithBrackets(Item item1)
        {

            int decimalPlace = 2;

            string string1 = MathHelper.RoundDec(item1.number, decimalPlace) + item1.unit;

            string eq = "(" + string1 + ")²";
            double ans = item1.number * item1.number;

            Item newItem = new Class.Item("Square", ans, "", eq);

            return newItem;
        }

        public static Item Cube(Item item1)
        {

            int decimalPlace = 2;

            string string1 = MathHelper.RoundDec(item1.number, decimalPlace) + item1.unit;

            string eq = string1 + "³";
            double ans = item1.number * item1.number * item1.number;

            Item newItem = new Class.Item("Cube", ans, "", eq);

            return newItem;
        }

        public static Item CubeWithBrackets(Item item1)
        {

            int decimalPlace = 2;

            string string1 = MathHelper.RoundDec(item1.number, decimalPlace) + item1.unit;

            string eq = "(" + string1 + ")³";
            double ans = item1.number * item1.number * item1.number;

            Item newItem = new Class.Item("Cube", ans, "", eq);

            return newItem;
        }

        public static Item Min(Item item1, Item item2)
        {
            if (item1.number <= item2.number)
            {
                Item newItem = item1;
                return newItem;
            }
            else
            {
                Item newItem = item2;
                return newItem;
            }
        }
        public static Item Min(Item item1, Item item2, Item item3)
        {
            return Min(Min(item1, item2), item3);
        }

    }
}
