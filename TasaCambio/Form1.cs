using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TasaCambio
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public static string ConexionErp = "Data Source=.;Initial Catalog=bd_erp_next;Persist Security Info=False;User ID=admin;Password=Admin@123";
        private DataTable dtDatos;

        public Form1()
        {

            InitializeComponent();

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            
            CargarGrid();

            dataGridViewTasaCambio.AllowUserToAddRows = true;
            dataGridViewTasaCambio.AllowUserToDeleteRows = true;
            dataGridViewTasaCambio.ReadOnly = false;
            dataGridViewTasaCambio.EditMode = DataGridViewEditMode.EditOnEnter;

            
            dataGridViewTasaCambio.RowValidated += DataGridViewTasaCambio_RowValidated;
            dataGridViewTasaCambio.DefaultValuesNeeded += DataGridViewTasaCambio_DefaultValuesNeeded;

            SetDataGridViewColumnProperties();
            dataGridViewTasaCambio.ClearSelection();
        }

        private void SetDataGridViewColumnProperties()
        {
            if (dataGridViewTasaCambio.Columns.Contains("tasa"))
            {
                dataGridViewTasaCambio.Columns["tasa"].ReadOnly = false;
                dataGridViewTasaCambio.Columns["tasa"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dataGridViewTasaCambio.Columns.Contains("fecha"))
            {
                dataGridViewTasaCambio.Columns["fecha"].ReadOnly = false;
                dataGridViewTasaCambio.Columns["fecha"].DefaultCellStyle.Format = "yyyy-MM-dd";
            }
            if (dataGridViewTasaCambio.Columns.Contains("id"))
            {
                dataGridViewTasaCambio.Columns["id"].ReadOnly = true;
                dataGridViewTasaCambio.Columns["id"].Visible = true;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void BotonNuevo_Click(object sender, EventArgs e)
        {
            dateTimePicker_fecha.Enabled = true;
            textBox_tasa.Enabled = true;
            textBox_tasa.Text = string.Empty;

            if (dataGridViewTasaCambio.AllowUserToAddRows)
            {
                if (dataGridViewTasaCambio.Rows.Count > 0 && !dataGridViewTasaCambio.Rows[dataGridViewTasaCambio.Rows.Count - 1].IsNewRow)
                {
                    dataGridViewTasaCambio.CurrentCell = dataGridViewTasaCambio.Rows[dataGridViewTasaCambio.Rows.Count - 1].Cells[0];
                    dataGridViewTasaCambio.BeginEdit(true);
                }
                else if (dataGridViewTasaCambio.Rows.Count > 0 && dataGridViewTasaCambio.Rows[dataGridViewTasaCambio.Rows.Count - 1].IsNewRow)
                {
                    MessageBox.Show("Ya puedes introducir un nuevo registro de tasa", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (dataGridViewTasaCambio.Rows.Count == 0)
                {
                    dataGridViewTasaCambio.Rows.Add();
                    dataGridViewTasaCambio.CurrentCell = dataGridViewTasaCambio.Rows[0].Cells[0];
                    dataGridViewTasaCambio.BeginEdit(true);
                }
            }
            else
            {
                MessageBox.Show("Para añadir nuevos registros, la propiedad 'AllowUserToAddRows' debe estar en true.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DataGridViewTasaCambio_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            if (e.Row.IsNewRow)
            {
                
                int fechaIndex = e.Row.DataGridView.Columns["fecha"].Index;
                e.Row.Cells[fechaIndex].Value = DateTime.Today;
            }
        }

        private void DataGridViewTasaCambio_RowValidated(object sender, DataGridViewCellEventArgs e)
        {

        }



        private void EliminarFila()
        {
            if (dataGridViewTasaCambio.SelectedRows.Count > 0)
            {
                if (!dataGridViewTasaCambio.Columns.Contains("id"))
                {
                    MessageBox.Show("La columna 'id' no se encontró en el DataGridView. No se puede eliminar el registro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int id = Convert.ToInt32(dataGridViewTasaCambio.SelectedRows[0].Cells["id"].Value);

                DialogResult dialogResult = MessageBox.Show($"¿Estás seguro de que quieres eliminar el registro con ID: {id}?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.No)
                {
                    return;
                }

                using (SqlConnection conexion = new SqlConnection(ConexionErp))
                {
                    try
                    {
                        conexion.Open();
                        string query = "DELETE FROM bd_erp_next.dbo.tasacambio WHERE id = @id";

                        using (SqlCommand cmd = new SqlCommand(query, conexion))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.ExecuteNonQuery();
                        }

                        CargarGrid();

                        MessageBox.Show("Registro eliminado correctamente.", "Eliminación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al eliminar: " + ex.Message, "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecciona una fila para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BotonEditar_Click(object sender, EventArgs e)
        {
            dataGridViewTasaCambio.Enabled = true;
            dataGridViewTasaCambio.ClearSelection();

            if (dataGridViewTasaCambio.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridViewTasaCambio.SelectedRows[0];
                dateTimePicker_fecha.Enabled = true;
                textBox_tasa.Enabled = true;
                dateTimePicker_fecha.Value = Convert.ToDateTime(selectedRow.Cells["fecha"].Value);
                textBox_tasa.Text = selectedRow.Cells["tasa"].Value.ToString();

               
                dataGridViewTasaCambio.ClearSelection();
            }
            else
            {
                MessageBox.Show("Selecciona una fila para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (SqlConnection conexion = new SqlConnection(ConexionErp))
            {
                try
                {
                    conexion.Open();
                    string query = "SELECT tasa, fecha FROM bd_erp_next.dbo.tasacambio";

                    using (SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion))
                    {
                        DataTable dt = new DataTable();
                        adaptador.Fill(dt);
                        MessageBox.Show("Datos actualizados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }

                
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

        }

        private void CargarGrid()


            

        {
            dateTimePicker_fecha.Enabled = false;
            textBox_tasa.Enabled = false;
            textBox_tasa.Text = string.Empty;
            dataGridViewTasaCambio.Enabled = false;



            using (SqlConnection conexion = new SqlConnection(ConexionErp))
            {
                try
                {
                    conexion.Open();
                    string query = "SELECT id, tasa, fecha FROM bd_erp_next.dbo.tasacambio";

                    using (SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion))
                    {
                        dtDatos = new DataTable();
                        adaptador.Fill(dtDatos);
                        dataGridViewTasaCambio.DataSource = dtDatos;
                        gridControl_tasacambio.DataSource = dtDatos;


                        
                        SetDataGridViewColumnProperties();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los datos: " + ex.Message, "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button_guardar_Click(object sender, EventArgs e)
        {

            ValidacionesDatos();

            double tasaValue;

            if (!double.TryParse(textBox_tasa.Text, out tasaValue))
            {
                MessageBox.Show("El valor de la tasa no puede estar vacia o debe de ser un numero valido.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            object fechaValue = dateTimePicker_fecha.Value;

            if (fechaValue != null && fechaValue != DBNull.Value)
            {
                using (SqlConnection conexion = new SqlConnection(ConexionErp))
                {
                    try
                    {
                        conexion.Open();
                        string query = "INSERT INTO bd_erp_next.dbo.tasacambio (tasa, fecha, idmoneda, idMoneda2) VALUES (@tasa, @fecha,@idmoneda, @idMoneda2); SELECT SCOPE_IDENTITY();";

                        using (SqlCommand cmd = new SqlCommand(query, conexion))
                        {
                            cmd.Parameters.AddWithValue("@tasa", tasaValue);
                            cmd.Parameters.AddWithValue("@fecha", fechaValue);
                            cmd.Parameters.AddWithValue("@idmoneda", 28);
                            cmd.Parameters.AddWithValue("@idMoneda2", 27);
                            object result = cmd.ExecuteScalar();

                            if (result != null && result != DBNull.Value)
                            {
                                int newId = Convert.ToInt32(result);
                                MessageBox.Show("Nuevo registro añadido correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Error: No se pudo obtener el ID del nuevo registro.", "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al añadir nuevo registro: " + ex.Message, "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        CargarGrid();
                        textBox_tasa.Enabled = true;
                        dateTimePicker_fecha.Enabled = true;
                    }
                }
            }
            else
            {
                MessageBox.Show("La fecha no puede estar vacía.", "Error de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            dataGridViewTasaCambio.ClearSelection();
            dataGridViewTasaCambio.Enabled = false;
        }
        

        private void BotonImportar_Click(object sender, EventArgs e)
        {
            EliminarFila1();
            dataGridViewTasaCambio.ClearSelection();
        }

        private void EliminarFila1()
        {

            dataGridViewTasaCambio.Enabled = true;
            if (dataGridViewTasaCambio.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGridViewTasaCambio.SelectedRows[0].Cells["id"].Value);

                using (SqlConnection conexion = new SqlConnection(ConexionErp))
                {
                    try
                    {
                        conexion.Open();
                        string query = "DELETE FROM bd_erp_next.dbo.tasacambio WHERE id = @id";

                        using (SqlCommand cmd = new SqlCommand(query, conexion))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.ExecuteNonQuery();
                        }

                        CargarGrid();

                        MessageBox.Show("Registro eliminado correctamente.");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al eliminar: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecciona una fila para eliminar.");
            }



        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void gridControl_tasacambio_Click(object sender, EventArgs e)
        {

        }

        private void ValidacionesDatos()
        {
            string fecha =dateTimePicker_fecha.Text.Trim();
            string tasa = textBox_tasa.Text.Trim();

            


            if (string.IsNullOrEmpty(fecha))
            {
                MessageBox.Show("La fecha no puede estar vacía.", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            
            if (!DateTime.TryParse(fecha, out DateTime fechaValue))
            {
                MessageBox.Show("La fecha debe ser una fecha válida.", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (fechaValue < new DateTime(2000, 1, 1) || fechaValue > DateTime.Today)
            {
                MessageBox.Show("La fecha debe estar entre el 01/01/2000 y la fecha actual.", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            

            
            

        }

        private void textBox_tasa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true; 
                MessageBox.Show("Por favor, introduce un número válido para la tasa.", "Entrada Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RecargarDatos()
        {
            CargarGrid();
            dateTimePicker_fecha.Enabled = false;
            textBox_tasa.Enabled = false;
            textBox_tasa.Text = string.Empty;
        }

        private void button_recargar_Click(object sender, EventArgs e)
        {
            RecargarDatos();
            dataGridViewTasaCambio.ClearSelection();
            dataGridViewTasaCambio.Enabled = false;
            MessageBox.Show("Registro cancelado.", "Recarga Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}