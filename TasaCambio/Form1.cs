using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;

namespace TasaCambio
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public static string ConexionErp = "Data Source=.;Initial Catalog=bd_erp_next;Persist Security Info=False;User ID=admin;Password=Admin@123";
        private DataTable dtDatos;
        int idrow = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarGrid();
        }

        

        private void BotonNuevo_Click(object sender, EventArgs e)
        {
            dateTimePicker_fecha.Enabled = true;
            textBox_tasa.Enabled = true;
            textBox_tasa.Text = string.Empty;
            idrow = 0;
            
            (gridControl_tasacambio.MainView as GridView)?.ClearSelection();
        }

        private void BotonEditar_Click(object sender, EventArgs e)
        {
            
            GridView view = gridControl_tasacambio.MainView as GridView;
            if (view == null)
            {
                return;
            }

            
            int[] selectedRows = view.GetSelectedRows();
            if (selectedRows.Length > 0)
            {
                int rowHandle = selectedRows[0];

                
                idrow = Convert.ToInt32(view.GetRowCellValue(rowHandle, "id"));

                
                textBox_tasa.Text = view.GetRowCellValue(rowHandle, "tasa").ToString();
                dateTimePicker_fecha.Value = Convert.ToDateTime(view.GetRowCellValue(rowHandle, "fecha"));

                
                dateTimePicker_fecha.Enabled = true;
                textBox_tasa.Enabled = true;

                

                textBox_tasa.Focus();
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una fila para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void actualizar()
        {
            if (idrow == 0)
            {
                MessageBox.Show("Seleccione un registro para actualizar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conexion = new SqlConnection(ConexionErp))
            {
                try
                {
                    string fecha = dateTimePicker_fecha.Text.Trim();
                    string tasa = textBox_tasa.Text.Trim();

                    conexion.Open();
                    string query = @"UPDATE bd_erp_next.dbo.tasacambio SET tasa = @tasa, fecha = @fecha WHERE id = @id";

                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@id", idrow);
                        cmd.Parameters.AddWithValue("@tasa", tasa);
                        cmd.Parameters.AddWithValue("@fecha", fecha);
                        object result = cmd.ExecuteNonQuery();

                        if (result != null && result != DBNull.Value && Convert.ToInt32(result) > 0)
                        {
                            CargarGrid();
                            MessageBox.Show("Datos actualizados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            idrow = 0; // Resetea la variable después de actualizar
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CargarGrid()
        {
            dateTimePicker_fecha.Enabled = false;
            textBox_tasa.Enabled = false;
            textBox_tasa.Text = string.Empty;
            idrow = 0;

            using (SqlConnection conexion = new SqlConnection(ConexionErp))
            {
                try
                {
                    conexion.Open();
                    string query = "SELECT id, tasa, fecha FROM bd_erp_next.dbo.tasacambio WHERE anulado = 0";

                    using (SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion))
                    {
                        dtDatos = new DataTable();
                        adaptador.Fill(dtDatos);
                        gridControl_tasacambio.DataSource = dtDatos;
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
            if (!ValidacionesDatos())
            {
                return;
            }

            if (idrow > 0)
            {
                actualizar();
            }
            else
            {
                guardarnuevo();
            }
        }

        private bool ValidacionesDatos()
        {
            string fechaString = dateTimePicker_fecha.Text.Trim();
            string tasaString = textBox_tasa.Text.Trim();
            double tasaValue;

            if (string.IsNullOrEmpty(fechaString))
            {
                MessageBox.Show("La fecha no puede estar vacía.", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!DateTime.TryParse(fechaString, out DateTime fechaValue))
            {
                MessageBox.Show("La fecha debe ser una fecha válida.", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (fechaValue < new DateTime(2000, 1, 1) || fechaValue > DateTime.Today)
            {
                MessageBox.Show("La fecha debe estar entre el 01/01/2000 y la fecha actual.", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!double.TryParse(tasaString, out tasaValue))
            {
                MessageBox.Show("El valor de la tasa no puede estar vacía o debe ser un número válido.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void guardarnuevo()
        {
            object fechaValue = dateTimePicker_fecha.Value;
            object tasaValue = textBox_tasa.Text.Trim();

            using (SqlConnection conexion = new SqlConnection(ConexionErp))
            {
                try
                {
                    conexion.Open();

                    string validacionQuery = "SELECT COUNT(*) FROM bd_erp_next.dbo.tasacambio WHERE fecha = @fecha AND anulado = 0";
                    using (SqlCommand validacionCmd = new SqlCommand(validacionQuery, conexion))
                    {
                        validacionCmd.Parameters.AddWithValue("@fecha", fechaValue);
                        int count = Convert.ToInt32(validacionCmd.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show("Ya existe una tasa de cambio registrada para esta fecha. No se puede agregar una fecha duplicada.", "Fecha Duplicada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string query = "INSERT INTO bd_erp_next.dbo.tasacambio (tasa, fecha, idmoneda, idMoneda2) VALUES (@tasa, @fecha, @idmoneda, @idMoneda2); SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@tasa", tasaValue);
                        cmd.Parameters.AddWithValue("@fecha", fechaValue);
                        cmd.Parameters.AddWithValue("@idmoneda", 28);
                        cmd.Parameters.AddWithValue("@idMoneda2", 27);
                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
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
                    textBox_tasa.Enabled = false;
                    dateTimePicker_fecha.Enabled = false;
                }
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
            MessageBox.Show("Registro cancelado.", "Recarga Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BotonEliminar_Click(object sender, EventArgs e)
        {
            GridView view = gridControl_tasacambio.MainView as GridView;
            if (view == null) return;

            int[] selectedRows = view.GetSelectedRows();
            if (selectedRows.Length > 0)
            {
                int rowHandle = selectedRows[0];
                int idToDelete = Convert.ToInt32(view.GetRowCellValue(rowHandle, "id"));

                if (MessageBox.Show("¿Estás seguro de que deseas eliminar este registro?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    EliminarRegistro(idToDelete);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un registro para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void EliminarRegistro(int id)
        {
            using (SqlConnection conexion = new SqlConnection(ConexionErp))
            {
                try
                {
                    conexion.Open();
                    string query = "UPDATE bd_erp_next.dbo.tasacambio SET anulado = 1 WHERE id = @id";

                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Registro eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarGrid();
                        }
                        else
                        {
                            MessageBox.Show("No se encontró el registro para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el registro: " + ex.Message, "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
        }
    }
}