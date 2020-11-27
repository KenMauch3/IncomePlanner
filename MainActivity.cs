using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;

namespace IncomePlanner
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText incomePerHourEditText;
        EditText workHourPerDayEditText;
        EditText taxRateEditText;
        EditText savingRateEditText;

        TextView workSummaryTextView;
        TextView grossIncomeTextView;
        TextView taxPayableTextView;
        TextView annualSavingsTextView;
        TextView spendableIncomeTextView;

        Button calculateButton;
        RelativeLayout resultLayout;

        bool inputCalculated = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            ConnectViews();
            resultLayout.Visibility = Android.Views.ViewStates.Gone;
        }

        void ConnectViews()
        {
            incomePerHourEditText = FindViewById<EditText>(Resource.Id.incomePerHourEditText);
            workHourPerDayEditText = FindViewById<EditText>(Resource.Id.workHoursPerDayEditText);
            taxRateEditText = FindViewById<EditText>(Resource.Id.taxRateEditText);
            savingRateEditText = FindViewById<EditText>(Resource.Id.savingsRateEditText);

            workSummaryTextView = FindViewById<TextView>(Resource.Id.workSummaryTextView);
            grossIncomeTextView = FindViewById<TextView>(Resource.Id.grossIncomeTextView);
            taxPayableTextView = FindViewById<TextView>(Resource.Id.taxPayableTextView);
            annualSavingsTextView = FindViewById<TextView>(Resource.Id.annualSavingsTextView);
            spendableIncomeTextView = FindViewById<TextView>(Resource.Id.spendableIncomeTextView);

            calculateButton = FindViewById<Button>(Resource.Id.calculateButton);
            resultLayout = FindViewById<RelativeLayout>(Resource.Id.resultLayout);

            calculateButton.Click += CalculateButton_Click;
        }

        private void CalculateButton_Click(object sender, System.EventArgs e)
        {
            if(inputCalculated)
            {
                inputCalculated = false;
                calculateButton.Text = "Calculate";
                ClearInput();
                return;
            }
            // TODO: add input validation
            // read the inputs
            double incomePerHour = double.Parse(incomePerHourEditText.Text);
            double workHoursPerDay = double.Parse(workHourPerDayEditText.Text);
            double taxRate = double.Parse(taxRateEditText.Text);
            double savingsRate = double.Parse(savingRateEditText.Text);

            // working 5 days per week, 50 weeks per year (assuming 2 weeks of vacation
            double workHoursPerYear = workHoursPerDay * (double) 5 * (double) 50;
            double annualIncome = workHoursPerYear * incomePerHour;
            double annualTax = annualIncome * taxRate / (double)100;
            double annualSavings = annualIncome * savingsRate / (double)100;
            double spendableIncome = annualIncome - annualTax - annualSavings;

            workSummaryTextView.Text = workHoursPerYear.ToString("#,##") + " Hours";
            grossIncomeTextView.Text = annualIncome.ToString("#,##") + " USD";
            taxPayableTextView.Text = annualTax.ToString("#,##") + " USD";
            annualSavingsTextView.Text = annualSavings.ToString("#,##") + " USD";
            spendableIncomeTextView.Text = spendableIncome.ToString("#,##") + " USD";
            resultLayout.Visibility = Android.Views.ViewStates.Visible;
            inputCalculated = true;
            calculateButton.Text = "Clear";
        }

        private void ClearInput()
        {
            incomePerHourEditText.Text = "";
            workHourPerDayEditText.Text = "";
            taxRateEditText.Text = "";
            savingRateEditText.Text = "";
            resultLayout.Visibility = Android.Views.ViewStates.Gone;
        }
    }
}