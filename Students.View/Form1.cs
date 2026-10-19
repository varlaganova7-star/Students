using System;
using System.Windows.Forms;
using Students.BusinessLogic; 

namespace Students.View
{
    public partial class Form1 : Form
    {
        private readonly StudentService _service = new StudentService();

        public Form1()
        {
            InitializeComponent();
            UpdateTitle();
            RefreshList();
        }

        private void UpdateTitle()
        {
            Text = $"Студенты (текущий режим: {_service.GetCurrentProviderName()})";
            string nextProvider = _service.GetCurrentProviderName() == "EF Core" ? "Dapper" : "EF Core";
            buttonToggleMode.Text = $"Переключить на {nextProvider}";
        }

        private void RefreshList()
        {
            try
            {
                dataGridViewStudents.DataSource = null;
              
                dataGridViewStudents.DataSource = _service.GetAll();
                UpdateTitle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки списка: " + ex.Message);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string firstName = textBoxFirstName.Text.Trim();
            string lastName = textBoxLastName.Text.Trim();
            string group = textBoxGroup.Text.Trim();

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(group))
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            try
            {
                _service.Add(firstName, lastName, group);
                RefreshList();

                textBoxFirstName.Clear();
                textBoxLastName.Clear();
                textBoxGroup.Clear();

                MessageBox.Show($"Студент добавлен через: {_service.GetCurrentProviderName()}", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления: " + ex.Message);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewStudents.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите студента для удаления!");
                return;
            }

            try
            {
                int id = Convert.ToInt32(dataGridViewStudents.SelectedRows[0].Cells["Id"].Value);
                _service.Remove(id);
                RefreshList();

                MessageBox.Show($"Студент удален через: {_service.GetCurrentProviderName()}", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка удаления: " + ex.Message);
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e) => RefreshList();

        private void buttonToggleMode_Click(object sender, EventArgs e)
        {
            try
            {
                _service.ToggleRepository();
                RefreshList();
                MessageBox.Show($"Режим успешно переключен на: {_service.GetCurrentProviderName()}", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка переключения режима: " + ex.Message);
            }
        }
    }
}