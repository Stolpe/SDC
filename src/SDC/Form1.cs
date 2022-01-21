using System.Data;
using System.Media;

namespace SDC;

public partial class Form1 : Form
{
    private readonly HashSet<Keys> _pressedKeys = new();
    private readonly Stack<GroupBox> _points = new();

    //dotnet publish -c Release -r win-x64 /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true
    public Form1()
    {
        InitializeComponent();

        this.comboBoxCategories.DataSource = Category.Categories;
        this.comboBoxCategories.DisplayMember = nameof(Category.Name);

        var buttons = this.tableLayoutPanel
            .Controls
            .OfType<GroupBox>()
            .SelectMany(gb => gb
                .Controls
                .OfType<Button>()
                .Select(b => (groupBox: gb, button: b)));

        foreach (var (groupBox, button) in buttons)
        {
            button.Click += (sender, e) => AddPoints((Button)sender!, groupBox);
        }
    }

    private void AddPoints(
        Button button,
        GroupBox groupBox)
    {
        var counter = groupBox
            .Controls
            .OfType<Label>()
            .First(l => StringComparer.OrdinalIgnoreCase.Equals(l.Tag, Tags.Counter));
        counter.Text = (decimal.Parse(counter.Text) + decimal.Parse(button.Text)).ToString();
        _points.Push(groupBox);
        this.btnUndo.Enabled = true;
    }

    private void Form1_KeyDown(
        object sender,
        KeyEventArgs e)
    {
        if (_pressedKeys.Add(e.KeyCode))
        {
            var button = e.KeyCode switch
            {
                Keys.NumPad1 => this.btnOne,
                Keys.NumPad2 => this.btnTwo,
                Keys.NumPad3 => this.btnThree,
                Keys.NumPad4 => this.btnFour,
                Keys.NumPad5 => this.btnFive,
                Keys.NumPad6 => this.btnSix,
                Keys.NumPad7 => this.btnSeven,
                Keys.NumPad8 => this.btnEight,
                Keys.NumPad9 => this.btnNine,
                _ => default
            };

            var groupBox = this.tableLayoutPanel
                .Controls
                .OfType<GroupBox>()
                .Where(gb => gb.Enabled == true)
                .FirstOrDefault(gb => gb
                    .Controls
                    .OfType<Button>()
                    .Any(b => b == button));

            if (button != default && groupBox != default)
            {
                AddPoints(button, groupBox);
            }
            else if (e.KeyCode == Keys.Subtract)
            {
                var lastAddedGroupBox = _points.Pop();
                RemovePoints(lastAddedGroupBox);
            }
            else
            {
                SystemSounds.Beep.Play();
            }
        }
        else
        {
            e.SuppressKeyPress = true;
        }
    }

    private void Form1_KeyUp(
        object sender,
        KeyEventArgs e)
    {
        _pressedKeys.Remove(e.KeyCode);
    }

    private void comboBoxCategories_SelectedIndexChanged(
        object sender,
        EventArgs e)
    {
        var category = (Category)((ComboBox)sender).SelectedItem;
        var deductions = category.Deductions;
        var groupBoxes = this.tableLayoutPanel
            .Controls
            .OfType<GroupBox>();
        var deductionGroupBoxes = Enumerable.Range(
                0,
                9)
            .Select(n => (
                groupBox: groupBoxes.ElementAt(n),
                deduction: deductions.ElementAtOrDefault(n)
            ));

        foreach (var (groupBox, deduction) in deductionGroupBoxes)
        {
            var nameLabel = groupBox.Controls
                .OfType<Label>()
                .First(l => StringComparer.OrdinalIgnoreCase.Equals(l.Tag, Tags.Name));
            var counterLabel = groupBox.Controls
                .OfType<Label>()
                .First(l => StringComparer.OrdinalIgnoreCase.Equals(l.Tag, Tags.Counter));
            var button = groupBox.Controls
                .OfType<Button>()
                .First();

            counterLabel.Text = "0";

            if (deduction != null)
            {
                groupBox.Enabled = true;
                nameLabel.Text = deduction.Name;
                button.Text = deduction.Points.ToString();
            }
            else
            {
                groupBox.Enabled = false;
                nameLabel.Text = "";
                button.Text = "";
            }
        }
    }

    private void btnReset_Click(
        object sender,
        EventArgs e)
    {
        var labels = this.tableLayoutPanel
            .Controls
            .OfType<GroupBox>()
            .SelectMany(gb => gb
                .Controls
                .OfType<Label>()
                .Where(l => StringComparer.OrdinalIgnoreCase.Equals(l.Tag, Tags.Counter)));

        foreach (var label in labels)
        {
            label.Text = "0";
        }
    }

    private void btnUndo_Click(
        object sender,
        EventArgs e)
    {
        var lastAddedGroupBox = this._points.Pop();

        RemovePoints(lastAddedGroupBox);
    }

    private void RemovePoints(
        GroupBox groupBox)
    {
        var button = groupBox
            .Controls
            .OfType<Button>()
            .First();
        var counter = groupBox
            .Controls
            .OfType<Label>()
            .First(l => StringComparer.OrdinalIgnoreCase.Equals(l.Tag, Tags.Counter));

        counter.Text = (decimal.Parse(counter.Text) - decimal.Parse(button.Text)).ToString();

        this.btnUndo.Enabled = _points.Any();
    }
}
