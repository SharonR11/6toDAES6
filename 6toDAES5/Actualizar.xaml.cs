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
    /// Lógica de interacción para Actualizar.xaml
    /// </summary>
    public partial class Actualizar : Window
    {
        private string connectionString = "Data Source=LAB1504-25\\SQLEXPRESS;Initial Catalog=Neptuno;User Id=userSharon;Password=010203";

        private Empleado empleado;

        public Actualizar(Empleado empleado)
        {
            InitializeComponent();

            this.empleado = empleado;
            txtIdEmpleado.Text = empleado.IdEmpleado.ToString();
            txtApellidos.Text = empleado.Apellidos;
            txtNombre.Text = empleado.Nombre;
            txtCargo.Text = empleado.Cargo;
            txtTratamiento.Text = empleado.Tratamiento;
            dpFechaNacimiento.SelectedDate = empleado.FechaNacimiento;
            dpFechaContratacion.SelectedDate = empleado.FechaContratacion;
            txtDireccion.Text = empleado.Direccion;
            txtCiudad.Text = empleado.Ciudad;
            txtRegion.Text = empleado.Region;
            txtCodPostal.Text = empleado.CodPostal;
            txtPais.Text = empleado.Pais;
            txtTelDomicilio.Text = empleado.TelDomicilio;
            txtExtension.Text = empleado.Extension;
            txtNotas.Text = empleado.Notas;
            txtJefe.Text = empleado.Jefe.ToString();
            txtSueldoBasico.Text = empleado.SueldoBasico.ToString();
        }


        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int idEmpleado = int.Parse(txtIdEmpleado.Text);
                string apellidos = txtApellidos.Text;
                string nombre = txtNombre.Text;
                string cargo = txtCargo.Text;
                string tratamiento = txtTratamiento.Text;
                DateTime fechaNacimiento = dpFechaNacimiento.SelectedDate.Value;
                DateTime fechaContratacion = dpFechaContratacion.SelectedDate.Value;
                string direccion = txtDireccion.Text;
                string ciudad = txtCiudad.Text;
                string region = txtRegion.Text;
                string codPostal = txtCodPostal.Text;
                string pais = txtPais.Text;
                string telDomicilio = txtTelDomicilio.Text;
                string extension = txtExtension.Text;
                string notas = txtNotas.Text;
                int jefe = int.Parse(txtJefe.Text);
                decimal sueldoBasico = decimal.Parse(txtSueldoBasico.Text);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("USP_ActualizarEmpleado", connection))
                    {

                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
                        command.Parameters.AddWithValue("@Apellidos", apellidos);
                        command.Parameters.AddWithValue("@Nombre", nombre);
                        command.Parameters.AddWithValue("@Cargo", cargo);
                        command.Parameters.AddWithValue("@Tratamiento", tratamiento);
                        command.Parameters.AddWithValue("@FechaNacimiento", fechaNacimiento);
                        command.Parameters.AddWithValue("@FechaContratacion", fechaContratacion);
                        command.Parameters.AddWithValue("@Direccion", direccion);
                        command.Parameters.AddWithValue("@Ciudad", ciudad);
                        command.Parameters.AddWithValue("@Region", region);
                        command.Parameters.AddWithValue("@CodPostal", codPostal);
                        command.Parameters.AddWithValue("@Pais", pais);
                        command.Parameters.AddWithValue("@TelDomicilio", telDomicilio);
                        command.Parameters.AddWithValue("@Extension", extension);
                        command.Parameters.AddWithValue("@Notas", notas);
                        command.Parameters.AddWithValue("@Jefe", jefe);
                        command.Parameters.AddWithValue("@SueldoBasico", sueldoBasico);

                        command.ExecuteNonQuery();
                        
                        Lista lista = new Lista();
                        lista.Show();
                        this.Close();
                    }
                }

                MessageBox.Show("Empleado actualizado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar empleado: " + ex.Message);
            }
        }
    }
}
