using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        //Payment Variables
        public double totalPayment = 0;
        public String payment = "";
        //Drinks size, 1 = small, 2 = medium, 3 = large
        public int drinkSizeState = 0;


        public Form1()
        {
            InitializeComponent();
        }

        //Method to add items to append items to screen
        public void addItem(String name, double price){
            libPurchaseMenu.Items.Add(name + " - $" + price);
            totalPayment += price;
            grpDrinks.Enabled = false;
            grpDrinkSizes.Enabled = true;
            if (chbStaffDiscount.Checked == false)
            {
                lblTotal.Text = "Total: $" + totalPayment;
                lblTotalGST.Text = "GST Total: $" + (totalPayment * 0.1);
            }
            else
            {
                lblTotal.Text = "Total: $" + (totalPayment*0.7);
                lblTotalGST.Text = "GST Total: $" + ((totalPayment*0.7) * 0.1);
            }
        }

        //method that clears the screen and resets prices
        public void resetOrder()
        {
            lblTotal.Text = "Total: $0";
            lblTotalGST.Text = "GST Total: $0";
            totalPayment = 0;
            libPurchaseMenu.Items.Clear();
            libPurchaseMenu.Items.Add("McDonalds Order:");
            libPurchaseMenu.Items.Add("--------------------------------");
            grpDrinkSizes.Enabled = true;
            grpDrinks.Enabled = false;
            drinkSizeState = 0;
        }

        //Exit buttons
        private void btnExitProgram_Click(object sender, EventArgs e) {Environment.Exit(0);}
        private void tlsExit_Click(object sender, EventArgs e) {Environment.Exit(0);}

        //Button that resets screen using reset method
        private void btnResetOrder_Click(object sender, EventArgs e)
        {
            if (grpPayment.Enabled == false)
            {
                resetOrder();
            }
            //If in payment menu reset back to ordering menu
            else
            {
                tabMenu.Enabled = true;
                grpPayment.Enabled = false;
                resetOrder();
                lblAmountDue.Text = "Total amount due:  $0";
                txtPayment.Text = "";
                payment = "";
                lblChange.Text = "Total Change: $";
            }
        }

        //Switch to payment menu and disable ordering menu
        private void btnPay_Click(object sender, EventArgs e)
        {
            if (chbStaffDiscount.Checked == false)
            {
                lblAmountDue.Text = "Total amount due: $" + totalPayment;
            }
            else {
                lblAmountDue.Text = "Total amount due: $" + (totalPayment*0.7);
            }
            
            tabMenu.Enabled = false;
            grpPayment.Enabled = true;
        }

        //Cancel payment without reseting order
        private void btnCancelPayment_Click(object sender, EventArgs e)
        {
            tabMenu.Enabled = true;
            grpPayment.Enabled = false;
            txtPayment.Text = "";
            payment = "";
            lblChange.Text = "Total Change: $";
        }

        //Reset everything back to normal
        private void btnCompleteOrder_Click(object sender, EventArgs e)
        {
            tabMenu.Enabled = true;
            grpPayment.Enabled = false;
            resetOrder();
            lblAmountDue.Text = "Total amount due:  $0";
            txtPayment.Text = "";
            payment = "";
            lblChange.Text = "Total Change: $";
        }

        //Shows about window with information about program
        private void tlsAbout_Click(object sender, EventArgs e)
        {
            AboutBox1 box = new AboutBox1();
            box.ShowDialog();
        }

        //If staff discount box changes state, change label accordingly
        private void chbStaffDiscount_CheckedChanged(object sender, EventArgs e)
        {
            if (chbStaffDiscount.Checked == false)
            {
                lblTotal.Text = "Total: $" + totalPayment;
                lblTotalGST.Text = "GST Total: $" + (totalPayment * 0.1);
            }
            else
            {
                lblTotal.Text = "Total: $" + (totalPayment * 0.7);
                lblTotalGST.Text = "GST Total: $" + ((totalPayment * 0.7) * 0.1);
            }
            if(grpPayment.Enabled == true){
                if (chbStaffDiscount.Checked == false)
                {
                    lblAmountDue.Text = "Total amount due:  $" + totalPayment;
                }
                else
                {
                    lblAmountDue.Text = "Total amount due:  $" + (totalPayment * 0.7);
                }
            }
        }

        //Method that takes keypad inputs and adds to string payment and displays accordingly
        public void Payment(String number)
        {
            payment += number;
            txtPayment.Text = payment;
        }

        //Numpad Key Clicks
        private void btn0_Click(object sender, EventArgs e) {Payment("0");}
        private void btn1_Click(object sender, EventArgs e) {Payment("1");}
        private void btn2_Click(object sender, EventArgs e) {Payment("2");}
        private void btn3_Click(object sender, EventArgs e) {Payment("3");}
        private void btn4_Click(object sender, EventArgs e) {Payment("4");}
        private void btn5_Click(object sender, EventArgs e) {Payment("5");}
        private void btn6_Click(object sender, EventArgs e) {Payment("6");}
        private void btn7_Click(object sender, EventArgs e) {Payment("7");}
        private void btn8_Click(object sender, EventArgs e) {Payment("8");}
        private void btn9_Click(object sender, EventArgs e) {Payment("9");}
        private void btnDecimal_Click(object sender, EventArgs e) {Payment(".");}

        //reset payment string and display
        private void btnClear_Click(object sender, EventArgs e)
        {
            payment = "";
            txtPayment.Text = "";
        }

        //Checks if payment is enough and formatted correctly, if it is displays change if needed.
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            double number;
            if (double.TryParse(payment, out number))
            {
                if ((number >= totalPayment) && chbStaffDiscount.Checked == false)
                {
                    lblChange.Text = "Total Change: $" + (number - totalPayment);
                }
                else if ((number >= (totalPayment*0.7)) && chbStaffDiscount.Checked == true)
                {
                    lblChange.Text = "Total Change: $" + Math.Round(number - (totalPayment*0.7),2);
                }
                else
                {
                    MessageBox.Show("Customer still owes $"+(totalPayment-number));
                }
            }
            else
            {
                MessageBox.Show("Please enter amount in correct 2 decimal format");
            }
        }

        //Ordering buttons

        //Breakfast
        private void btnSausageEggMcMuffin_Click(object sender, EventArgs e) {addItem("Sausage & Egg McMuffin", 2.80);}
        private void btnBaconEggMcMuffin_Click(object sender, EventArgs e) { addItem("Bacon & Egg McMuffin", 2.80); }
        private void btnSausageMcMuffin_Click(object sender, EventArgs e) { addItem("Sausage McMuffin", 2.25); }
        private void btnAngusEggBreakkieRoll_Click(object sender, EventArgs e) { addItem("Angus & Egg Brekkie Roll", 3.20); }
        private void btnDoubleBaconBBQRoll_Click(object sender, EventArgs e) { addItem("Double Bacon & BBQ Roll", 3.50); }
        private void btnEnglishMcMuffin_Click(object sender, EventArgs e) { addItem("English McMuffin", 2.50); }
        private void btnHotcakesButterSyrup_Click(object sender, EventArgs e) { addItem("Hotcakes with Butter & Syrup", 4.00); }
        private void btnHashbrowns_Click(object sender, EventArgs e) { addItem("Hash Brown", 0.75); }
        private void btnEnglishBrekkieWrap_Click(object sender, EventArgs e) { addItem("English Brekkie Wrap", 3.00); }
        private void btnBaconEggTomatoWrap_Click(object sender, EventArgs e) { addItem("Bacon, Egg & Tomato Wrap", 3.20); }
        private void btnTheSpainBrekkieWrap_Click(object sender, EventArgs e) { addItem("The Spain Brekkie Wrap", 3.00); }


        //Burgers
        private void btnTheBrazilBurger_Click(object sender, EventArgs e) { addItem("The Brazil Burger", 2.80); }
        private void btnBigMac_Click(object sender, EventArgs e) { addItem("Big Mac", 3.30); }
        private void btnGrandAngus_Click(object sender, EventArgs e) { addItem("Grand Angus", 3.70); }
        private void btnMightyAngus_Click(object sender, EventArgs e) { addItem("Mighty Angus", 4.20); }
        private void btnQuarterPounder_Click(object sender, EventArgs e) { addItem("Quarter Pounder", 1.50); }
        private void btnDoubleQuarterPounder_Click(object sender, EventArgs e) { addItem("Double Quarter Pounder", 2.20); }
        private void btnCheeseburger_Click(object sender, EventArgs e) { addItem("Cheeseburger", 1.50); }
        private void btnDoubleCheeseburger_Click(object sender, EventArgs e) { addItem("Double Cheeseburger", 2.20); }
        private void btnHamburger_Click(object sender, EventArgs e) { addItem("Hamburger", 2.50); }
        private void btnMcChamp_Click(object sender, EventArgs e) { addItem("McChamp", 2.75); }
        private void btnMcSpicy_Click(object sender, EventArgs e) { addItem("McSpicy", 2.75); }
        private void btnMcGrilled_Click(object sender, EventArgs e) { addItem("McGrilled", 2.75); }
        private void btnMcChicken_Click(object sender, EventArgs e) { addItem("McChicken", 2.75); }
        private void btntChickenNmayo_Click(object sender, EventArgs e) { addItem("Chicken 'n' Mayo", 3.00); }
        private void btnChickenNcheese_Click(object sender, EventArgs e) { addItem("Chicken 'n' Cheese", 3.00); }
        private void btnFiletOfish_Click(object sender, EventArgs e) { addItem("Filet-0-Fish", 2.80); }
        private void btnTheFranceBurger_Click(object sender, EventArgs e) { addItem("The France Burger", 2.80); }


        //Drink Size Method
        public void drinkSize(string size)
        {
            if (size == "S") drinkSizeState = 1;
            else if (size == "M") drinkSizeState = 2;
            else if (size == "L") drinkSizeState = 3;
            grpDrinkSizes.Enabled = false;
            grpDrinks.Enabled = true;
        }

        //drinks - Size
        private void btnDrinkSmall_Click(object sender, EventArgs e) { drinkSize("S"); }
        private void btnDrinkMedium_Click(object sender, EventArgs e) { drinkSize("M"); }
        private void btnDrinkLarge_Click(object sender, EventArgs e) { drinkSize("L"); }


        //drinks - Type
        //Calculates price by getting a multiplyer from the drink size to get the new price
        private void btnCoke_Click(object sender, EventArgs e) 
        {
            double price = 2;
            if (drinkSizeState == 1) addItem("Small Coke", price);
            else if (drinkSizeState == 2) addItem("Medium Coke", price * 1.2);
            else if (drinkSizeState == 3) addItem("Large Coke", price * 1.4);
        }
        private void btnDietCoke_Click(object sender, EventArgs e)
        {
            double price = 2;
            if (drinkSizeState == 1) addItem("Small Diet Coke", price);
            else if (drinkSizeState == 2) addItem("Medium Diet Coke", price * 1.2);
            else if (drinkSizeState == 3) addItem("Large Diet Coke", price * 1.4);
        }
        private void btnCokeZero_Click(object sender, EventArgs e)
        {
            double price = 2;
            if (drinkSizeState == 1) addItem("Small Coke Zero", price);
            else if (drinkSizeState == 2) addItem("Medium Coke Zero", price * 1.2);
            else if (drinkSizeState == 3) addItem("Large Coke Zero", price * 1.4);
        }
        private void btnSprite_Click(object sender, EventArgs e)
        {
            double price = 2;
            if (drinkSizeState == 1) addItem("Small Sprite", price);
            else if (drinkSizeState == 2) addItem("Medium Sprite", price * 1.2);
            else if (drinkSizeState == 3) addItem("Large Sprite", price * 1.4);
        }
        private void btnFanta_Click(object sender, EventArgs e)
        {
            double price = 2;
            if (drinkSizeState == 1) addItem("Small Fanta", price);
            else if (drinkSizeState == 2) addItem("Medium Fanta", price * 1.2);
            else if (drinkSizeState == 3) addItem("Large Fanta", price * 1.4);
        }
        private void btnFrozenFanta_Click(object sender, EventArgs e)
        {
            double price = 2.5;
            if (drinkSizeState == 1) addItem("Small Frozen Fanta", price);
            else if (drinkSizeState == 2) addItem("Medium Frozen Fanta", price * 1.2);
            else if (drinkSizeState == 3) addItem("Large Frozen Fanta", price * 1.4);
        }
        private void btnFrozenCoke_Click(object sender, EventArgs e)
        {
            double price = 2.5;
            if (drinkSizeState == 1) addItem("Small Frozen Coke", price);
            else if (drinkSizeState == 2) addItem("Medium Frozen Coke", price * 1.2);
            else if (drinkSizeState == 3) addItem("Large Frozen Coke", price * 1.4);
        }
        private void btnFrozenFantaRaspberry_Click(object sender, EventArgs e)
        {
            double price = 2.5;
            if (drinkSizeState == 1) addItem("Small Frozen Raspberry Fanta", price);
            else if (drinkSizeState == 2) addItem("Medium Frozen Raspberry Fanta", price * 1.2);
            else if (drinkSizeState == 3) addItem("Large Frozen Raspberry Fanta", price * 1.4);
        }
        private void btnChocolateShake_Click(object sender, EventArgs e)
        {
            double price = 3;
            if (drinkSizeState == 1) addItem("Small Chocolate Shake", price);
            else if (drinkSizeState == 2) addItem("Medium Chocolate Shake", price * 1.2);
            else if (drinkSizeState == 3) addItem("Large Chocolate Shake", price * 1.4);
        }
        private void btnStrawberryShake_Click(object sender, EventArgs e)
        {
            double price = 3;
            if (drinkSizeState == 1) addItem("Small Strawberry Shake", price);
            else if (drinkSizeState == 2) addItem("Medium Strawberry Shake", price * 1.2);
            else if (drinkSizeState == 3) addItem("Large Strawberry Shake", price * 1.4);
        }
        private void btnVanillaShake_Click(object sender, EventArgs e)
        {
            double price = 3;
            if (drinkSizeState == 1) addItem("Small Vanilla Shake", price);
            else if (drinkSizeState == 2) addItem("Medium Vanilla Shake", price * 1.2);
            else if (drinkSizeState == 3) addItem("Large Vanilla Shake", price * 1.4);
        }
        private void btnWater_Click(object sender, EventArgs e)
        {
            double price = 2;
            if (drinkSizeState == 1) addItem("Small Water", price);
            else if (drinkSizeState == 2) addItem("Medium Water", price * 1.2);
            else if (drinkSizeState == 3) addItem("Large Water", price * 1.4);
        }


        //Desserts
        private void btnHotFudgeSundae_Click(object sender, EventArgs e) { addItem("Hot Fudge Sundae",1.5); }
        private void btnCaramelSundae_Click(object sender, EventArgs e) { addItem("Caramel Sundae", 1.5); }
        private void btnStrawberrySundae_Click(object sender, EventArgs e) { addItem("Strawberry Sundae", 1.5); }
        private void btnPlainSundae_Click(object sender, EventArgs e) { addItem("Plain Sundae", 1.0); }
        private void btnSoftServeCone_Click(object sender, EventArgs e) { addItem("Soft Serve Cone", 0.75); }
        private void btnTheItalyMcFlurry_Click(object sender, EventArgs e) { addItem("The Italy McFlurry", 2); }
        private void btnBubblegumSquashMcFlurry_Click(object sender, EventArgs e) { addItem("Bubblegum Squash McFlurry", 2); }
        private void btnOreoCookieMcFlurry_Click(object sender, EventArgs e) { addItem("Oreo Cookie McFlurry", 2); }
        private void btnMnMMcFlurry_Click(object sender, EventArgs e) { addItem("M&M's MINIS McFlurry", 2); }
        private void btnCrunchyMcFlurry_Click(object sender, EventArgs e) { addItem("Crunchy McFlurry", 2); }
        private void btnTheEnglandPie_Click(object sender, EventArgs e) { addItem("The England Pie", 2.5); }
        private void btnHotApplePie_Click(object sender, EventArgs e) { addItem("Hot Apple Pie", 2.2); }
        private void btnMcdonaldsCookies_Click(object sender, EventArgs e) { addItem("McDonalds Cookies", 2); }


        //Combo's
        private void btnHappyMeal_Click(object sender, EventArgs e) { addItem("Happy Meal",6); }
        private void btnFamilyValue_Click(object sender, EventArgs e) { addItem("Family Value Dinner Box", 15); }
        private void btnFamilyFavourites_Click(object sender, EventArgs e) { addItem("Family Favourites Dinner Box", 20); }
    }
}
