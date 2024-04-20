using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _6toDAES5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        
        private string connectionString = "Data Source=LAB1504-25\\SQLEXPRESS;Initial Catalog=Neptuno;User Id=userSharon;Password=010203";
        /// Al eliminar esta cadena debo cambiar el sqlconection

        public MainWindow()
        {
            InitializeComponent();
        }
        private void InsertarEmpleado()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                //sin static ...using (SqlConnection connection = new SqlConnection((new BaseDatos()).connectionString)
                //using (SqlConnection connection = new SqlConnection(BaseDatos.connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("USP_InsertarEmpleado", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@IdEmpleado", Convert.ToInt32(txtIdEmpleado.Text));
                        command.Parameters.AddWithValue("@Apellidos", txtApellidos.Text);
                        command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        command.Parameters.AddWithValue("@Cargo", txtCargo.Text);
                        command.Parameters.AddWithValue("@Tratamiento", txtTratamiento.Text);
                        command.Parameters.AddWithValue("@FechaNacimiento", dpFechaNacimiento.SelectedDate);
                        command.Parameters.AddWithValue("@FechaContratacion", dpFechaContratacion.SelectedDate);
                        command.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
                        command.Parameters.AddWithValue("@Ciudad", txtCiudad.Text);
                        command.Parameters.AddWithValue("@Region", txtRegion.Text);
                        command.Parameters.AddWithValue("@CodPostal", txtCodPostal.Text);
                        command.Parameters.AddWithValue("@Pais", txtPais.Text);
                        command.Parameters.AddWithValue("@TelDomicilio", txtTelDomicilio.Text);
                        command.Parameters.AddWithValue("@Extension", txtExtension.Text);
                        command.Parameters.AddWithValue("@Notas", txtNotas.Text);
                        command.Parameters.AddWithValue("@Jefe", Convert.ToInt32(txtJefe.Text));
                        command.Parameters.AddWithValue("@SueldoBasico", Convert.ToDecimal(txtSueldoBasico.Text));


                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Empleado insertado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar empleado: " + ex.Message);
            }
        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            InsertarEmpleado();
            Lista listaWindow = new Lista();
            listaWindow.Show();
            this.Close();
        }
    }
}