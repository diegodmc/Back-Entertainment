namespace Back_Entertainment.Models
{
  public class WalletCurrent
    {
        public int Id { get; set; }
        public int CodeWallet { get; set; }
        public string DateUpdate { get; set; }
        public string Email { get; set; }
        public string FirstAction { get; set; }
        public string FirstPctAction { get; set; }
        public string FirstPrcAction { get; set; }
        public string FirstPrcActionCurrent { get; set;}
        public string SecondAction { get; set; }
        public string SecondPctAction { get; set; }
        public string SecondPrcAction { get; set; }

        public string SecondPrcActionCurrent { get; set; }
        public string ThirdAction { get; set; }
        public string ThirdPctAction { get; set; }
        public string ThirdPrcAction { get; set; }

        public string ThirdPrcActionCurrent { get; set; }
        public string FourthAction { get; set; }
        public string FourthPctAction { get; set; }
        public string FourthPrcAction { get; set; }

        public string FourthPrcActionCurrent { get; set; }
        public string FifthAction { get; set; }
        public string FifthPctAction { get; set; }
        public string FifthPrcAction { get; set; }

        public string FifthPrcActionCurrent { get; set; }
        public string Balance { get; set; }
    }
}