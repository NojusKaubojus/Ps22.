using Microsoft.Win32;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

public partial class MainWindow : Window
{
    private DataEncryption dataEncryption;

    public MainWindow()
    {
        InitializeComponent();
        dataEncryption = new DataEncryption(new AesManaged());
    }

    private void BrowseButton_Click(object sender, RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog();
        if (openFileDialog.ShowDialog() == true)
        {
            FilePathTextBox.Text = openFileDialog.FileName;
        }
    }

    private async void EncryptButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            string password = PasswordBox.Password;
            string filePath = FilePathTextBox.Text;

            // Patikrinimas, ar slaptažodis ir kelias nėra tuščias
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Please enter both password and file path.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            byte[] fileContent = await Task.Run(() => File.ReadAllBytes(filePath));
            byte[] encryptedData = await Task.Run(() => dataEncryption.Encrypt(fileContent, password));

            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Encrypted Files (*.enc)|*.enc";
            if (saveFileDialog.ShowDialog() == true)
            {
                await Task.Run(() => File.WriteAllBytes(saveFileDialog.FileName, encryptedData));
                MessageBox.Show($"File encrypted successfully! Encrypted file saved as: {saveFileDialog.FileName}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error during encryption: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async void DecryptButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            string password = PasswordBox.Password;
            string filePath = FilePathTextBox.Text;

            // Patikrinimas, ar slaptažodis ir kelias nėra tuščias
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Please enter both password and file path.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            byte[] encryptedData = await Task.Run(() => File.ReadAllBytes(filePath));
            byte[] decryptedData = await Task.Run(() => dataEncryption.Decrypt(encryptedData, password));

            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Decrypted Files|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                await Task.Run(() => File.WriteAllBytes(saveFileDialog.FileName, decryptedData));
                MessageBox.Show($"File decrypted successfully! Decrypted file saved as: {saveFileDialog.FileName}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error during decryption: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
