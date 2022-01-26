using System;
using System.Collections;
using System.Windows.Forms;
using CDR.Decoder;
using CDR.Decoder.Parselets;
using CDR.Schema;

namespace Decoder
{
    public partial class FormatterSettingsForm : Form
    {
        private enum ElementSorterType
        {
            ByPath = 0,
            ByElementName = 1
        }

        private class ElementsListViewSorter : IComparer
        {
            private ElementPathComparer _pathSorter;

            public ElementsListViewSorter()
            {
                _pathSorter = new ElementPathComparer();
                SortBy = ElementSorterType.ByPath;
                Sorting = SortOrder.Ascending;
            }

            public ElementSorterType SortBy { get; set; }
            public SortOrder Sorting { get; set; }

            #region IComparer Members

            public int Compare(object x, object y)
            {
                ListViewItem e1 = x as ListViewItem;
                ListViewItem e2 = y as ListViewItem;

                int result = 0;

                if ((e1 != null) && (e2 != null))
                {
                    switch (SortBy)
                    {
                        case ElementSorterType.ByPath:
                            result = _pathSorter.Compare(e1.Text, e2.Text);
                            break;
                        case ElementSorterType.ByElementName:
                            result = String.Compare(e1.SubItems[1].Text, e2.SubItems[1].Text);
                            break;
                    }
                }

                if (Sorting == SortOrder.Descending)
                    result = -result;

                return result;
            }

            #endregion
        }

        private RecordFormatterSettings _settings;
        private ElementsListViewSorter _sorter;

        public FormatterSettingsForm(RecordFormatterSettings settings)
        {
            InitializeComponent();

            _sorter = new ElementsListViewSorter();

            elementsListView.ListViewItemSorter = _sorter;

            _settings = (settings == null) ? new RecordFormatterSettings() : settings.Clone();

            columnsListView.VirtualListSize = _settings.ColumnCount;

            defSchemaComboBox.Items.AddRange(CdrDefinitionProvider.Instance.AvailableSchemas());
            defSchemaComboBox.SelectedItem = CdrDefinitionProvider.Instance.CurrentSchema;

            ShowSchemaElements();
        }

        public RecordFormatterSettings FormatterSettings
        {
            get { return _settings; }
        }

        private void ShowSchemaElements()
        {
            valTypeListBox.BeginUpdate();
            valTypeListBox.Items.Clear();
            valTypeListBox.EndUpdate();

            elementsListView.BeginUpdate();

            elementsListView.Items.Clear();

            CdrDefinitionSchema schema = CdrDefinitionProvider.Instance.GetSchema((string)defSchemaComboBox.SelectedItem);

            ListViewItem item;

            foreach (CdrElementDefinition def in schema.Definitions.Values)
            {
                if (def.Parselet.Length > 0)
                {
                    item = new ListViewItem(def.Path);
                    item.SubItems.Add((def.Name.Length > 0) ? def.Name : def.Path);
                    item.SubItems.Add(def.Parselet);
                    item.SubItems.Add(def.ValueType);
                    elementsListView.Items.Add(item);
                }
            }

            elementsListView.EndUpdate();
        }

        private void defSchemaComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ShowSchemaElements();
        }

        private void elementsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (elementsListView.SelectedItems.Count > 0)
            {
                IParselet parselet = ParseletProvider.Instance.FindParselet(elementsListView.SelectedItems[0].SubItems[2].Text);

                valTypeListBox.BeginUpdate();
                valTypeListBox.Items.Clear();
                if (parselet != null)
                {
                    valTypeListBox.Items.AddRange(parselet.GetValueTypes());
                    if (String.IsNullOrEmpty(elementsListView.SelectedItems[0].SubItems[3].Text))
                    {
                        valTypeListBox.SelectedItem = parselet.DefaultValueType;
                    }
                    else
                    {
                        valTypeListBox.SelectedItem = elementsListView.SelectedItems[0].SubItems[3].Text;
                    }
                }
                valTypeListBox.EndUpdate();
            }
        }

        private void elementsListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column > 1)
                return;

            if (e.Column == (int)_sorter.SortBy)
            {
                _sorter.Sorting = (_sorter.Sorting == SortOrder.Ascending) ? SortOrder.Descending : SortOrder.Ascending;
            }
            else
            {
                _sorter.SortBy = (ElementSorterType)e.Column;
            }
            elementsListView.Sort();
        }

        private void delButton_Click(object sender, EventArgs e)
        {
            if (columnsListView.SelectedIndices.Count > 0)
            {
                columnsListView.BeginUpdate();
                int idx = columnsListView.SelectedIndices[0];

                _settings.RemoveColumnAt(idx);
                columnsListView.VirtualListSize--;

                if (idx >= _settings.ColumnCount) idx--;

                if (idx >= 0) columnsListView.Items[idx].Selected = true;
                columnsListView.EndUpdate();
            }
        }

        private void upButton_Click(object sender, EventArgs e)
        {
            if ((columnsListView.SelectedIndices.Count > 0) && (columnsListView.SelectedIndices[0] > 0))
            {
                columnsListView.BeginUpdate();

                int idx = columnsListView.SelectedIndices[0];
                FormatterColumn col = _settings.GetColumnAt(idx);
                _settings.RemoveColumnAt(idx);
                _settings.InsertColumn(--idx, col.Name, col.Definition);

                columnsListView.Items[idx].Selected = true;
                columnsListView.EnsureVisible(idx);

                columnsListView.EndUpdate();
            }
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            if ((columnsListView.SelectedIndices.Count > 0) && (columnsListView.SelectedIndices[0] < _settings.ColumnCount - 1))
            {
                columnsListView.BeginUpdate();

                int idx = columnsListView.SelectedIndices[0];
                FormatterColumn col = _settings.GetColumnAt(idx);
                _settings.RemoveColumnAt(idx);
                _settings.InsertColumn(++idx, col.Name, col.Definition);

                columnsListView.Items[idx].Selected = true;
                columnsListView.EnsureVisible(idx);

                columnsListView.EndUpdate();
            }
        }

        private void addColButton_Click(object sender, EventArgs e)
        {
            if (elementsListView.SelectedIndices.Count > 0)
            {
                columnsListView.BeginUpdate();

                ListViewItem item = elementsListView.SelectedItems[0];
                CdrElementDefinition def = new CdrElementDefinition(
                            item.Text,
                            item.SubItems[1].Text,
                            item.SubItems[2].Text,
                            (string)valTypeListBox.SelectedItems[0]
                            );
                int idx;

                if (columnsListView.SelectedIndices.Count > 0)
                {
                    idx = _settings.InsertColumn(columnsListView.SelectedIndices[0] + 1, colHdrText.Text, def);
                }
                else
                {
                    idx = _settings.AddColumn(colHdrText.Text, def);
                }

                if (idx >= 0)
                {
                    columnsListView.VirtualListSize++;
                    columnsListView.Items[idx].Selected = true;
                    columnsListView.EnsureVisible(idx);
                    columnsListView.EndUpdate();
                }
                else
                {
                    columnsListView.EndUpdate();
                    MessageBox.Show(String.Format("Column with name \"{0}\" already exists!", colHdrText.Text), "Add new column", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void columnsListView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            FormatterColumn col = _settings.GetColumnAt(e.ItemIndex);
            e.Item = new ListViewItem(col.Name);
            e.Item.SubItems.Add(String.Format("{0} - {1} [{2}]", col.Definition.Path, col.Definition.Name, col.Definition.ValueType));
        }

        private void valTypeListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            colHdrText.Text = String.Concat(elementsListView.SelectedItems[0].SubItems[1].Text, ".", (string)valTypeListBox.SelectedItems[0]);
        }
    }
}
