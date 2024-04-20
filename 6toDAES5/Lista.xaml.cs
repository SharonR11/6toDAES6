using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _6toDAES5
{
    /// <summary>
    /// Lógica de interacción para Lista.xaml
    /// </summary>
    public partial class Lista : Window
    {
        private string connectionString = "Data Source=LAB1504-25\\SQLEXPRESS;Initial Catalog=Neptuno;User Id=userSharon;Password=010203";

        public Lista()
        {
            InitializeComponent();
            MostrarEmpleados();
        }
        private void MostrarEmpleados()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("USP_ListarEmpleados", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Empleado empleado = new Empleado
                                {
                                    //IdEmpleado = reader.GetInt32(0),
                                    //Apellidos = reader.GetString(1),
                                    //Nombre = reader.GetString(2),
                                    //Cargo = reader.GetString(3),
                                    //Tratamiento = reader.GetString(4),
                                    //FechaNacimiento = reader.GetDateTime(5),
                                    //FechaContratacion = reader.GetDateTime(6),
                                    //Direccion = reader.GetString(7),
                                    //Ciudad = reader.GetString(8),
                                    //Region = reader.GetString(9),
                                    //CodPostal = reader.GetString(10),
                                    //Pais = reader.GetString(11),
                                    //TelDomicilio = reader.GetString(12),
                                    //Extension = reader.GetString(13),
                                    //Notas = reader.GetString(14),
                                    //Jefe = reader.GetInt32(15),
                                    //SueldoBasico = reader.GetDecimal(16)

                                    IdEmpleado = reader.GetInt32(0),
                                    Apellidos = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                                    Nombre = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                    Cargo = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                                    Tratamiento = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                                    FechaNacimiento = reader.IsDBNull(5) ? DateTime.MinValue : reader.GetDateTime(5),
                                    FechaContratacion = reader.IsDBNull(6) ? DateTime.MinValue : reader.GetDateTime(6),
                                    Direccion = reader.IsDBNull(7) ? string.Empty : reader.GetString(7),
                                    Ciudad = reader.IsDBNull(8) ? string.Empty : reader.GetString(8),
                                    Region = reader.IsDBNull(9) ? string.Empty : reader.GetString(9),
                                    CodPostal = reader.IsDBNull(10) ? string.Empty : reader.GetString(10),
                                    Pais = reader.IsDBNull(11) ? string.Empty : reader.GetString(11),
                                    TelDomicilio = reader.IsDBNull(12) ? string.Empty : reader.GetString(12),
                                    Extension = reader.IsDBNull(13) ? string.Empty : reader.GetString(13),
                                    Notas = reader.IsDBNull(14) ? string.Empty : reader.GetString(14),
                                    Jefe = reader.IsDBNull(15) ? 0 : reader.GetInt32(15),
                                    SueldoBasico = reader.IsDBNull(16) ? 0 : reader.GetDecimal(16)
                                };
                                dataGrid.Items.Add(empleado);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message);
            }
        }
        private void btnRegistrarEmpleado_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            Empleado empleado = (Empleado)dataGrid.SelectedItem;
            if (empleado != null)
            {
                Actualizar actualizar = new Actualizar(empleado);
                actualizar.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un empleado para editar.");
            }
        }
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Empleado empleado = (Empleado)dataGrid.SelectedItem;
            if (empleado != null)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand("USP_EliminarEmpleado", connection))
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@IdEmpleado", empleado.IdEmpleado);

                            command.ExecuteNonQuery();
                        }

                        MessageBox.Show("Empleado eliminado correctamente.");

                        dataGrid.Items.Remove(empleado);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar empleado: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un empleado para eliminar.");
            }
        }


    }
}
