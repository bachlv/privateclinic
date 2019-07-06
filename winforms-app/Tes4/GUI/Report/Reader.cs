using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraCharts;

namespace Tes4.GUI.Report
{
    public partial class Reader : DevExpress.XtraEditors.XtraForm
    {
        public Income income;
        public List<SeriesPoint> series_data;
        ChartControl chartControl1;
        public Reader(List<SeriesPoint> series)
        {
            InitializeComponent();
            series_data = new List<SeriesPoint>();
            income = new Income();
            series_data = series;
            create_Bar_Chart();
        }
        public void create_Bar_Chart()
        {
            chartControl1 = new ChartControl();
            Series series1 = new Series("Doanh số", ViewType.Bar);

            //Legend Format
            chartControl1.Legend.Title.Alignment = StringAlignment.Near;
            chartControl1.Legend.Visible = true;
            chartControl1.Legend.Title.MaxLineCount = 3;
            chartControl1.Legend.Title.WordWrap = true;

            foreach (SeriesPoint se in series_data)
            {
                series1.Points.Add(se);
            }
            chartControl1.Series.Add(series1);

            XYDiagram diagram = (XYDiagram)chartControl1.Diagram;
            ((XYDiagram)chartControl1.Diagram).AxisX.QualitativeScaleOptions.AutoGrid = true;
            ((XYDiagram)chartControl1.Diagram).AxisX.QualitativeScaleOptions.GridSpacing = 3;
            AxisLabel axisLabel = ((XYDiagram)chartControl1.Diagram).AxisX.Label;
            
            //Label settings column X
            diagram.AxisX.WholeRange.Auto = true;
            axisLabel.TextPattern = "{A:dd-MM}";
            axisLabel.ResolveOverlappingOptions.AllowHide = true;
            axisLabel.ResolveOverlappingOptions.AllowRotate = true;
            axisLabel.ResolveOverlappingOptions.AllowStagger = true;
            axisLabel.ResolveOverlappingOptions.MinIndent = 5;
            axisLabel.Staggered = true;
            chartControl1.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
            chartControl1.Legend.AlignmentVertical = LegendAlignmentVertical.Top;
          
            ((XYDiagram)chartControl1.Diagram).AxisY.Visibility = DevExpress.Utils.DefaultBoolean.True;

            //Display text On each colummn
            chartControl1.Series[0].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            BarSeriesLabel seriesLabel = chartControl1.Series[0].Label as BarSeriesLabel;
            seriesLabel.BackColor = Color.White;
            seriesLabel.Border.Color = Color.DarkSlateGray;
            seriesLabel.Font = new Font("Tahoma", 10, FontStyle.Regular);
            seriesLabel.Position = BarSeriesLabelPosition.TopInside;
            seriesLabel.TextOrientation = TextOrientation.Horizontal;
            seriesLabel.TextPattern = "{V}";

            chartControl1.Dock = DockStyle.Fill;
            this.Controls.Add(chartControl1);
        }
    }
}