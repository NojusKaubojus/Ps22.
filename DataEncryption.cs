public class DataEncryption
{
    private SymmetricAlgorithm algorithm;

    public DataEncryption(SymmetricAlgorithm algorithm)
    {
        this.algorithm = algorithm;

        // Įterpkite papildomą konfigūraciją pagal poreikį
        algorithm.KeySize = 256;
        algorithm.Mode = CipherMode.CFB;
    }

    public byte[] Encrypt(byte[] data, string password)
    {
        try
        {
            // Implementuokite šifravimo logiką naudodami 'algorithm'
            // ...

            byte[] encryptedData = null; // Pakeiskite į šifravimo rezultatą

            return encryptedData;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Encryption error: {ex.Message}");
            throw; // Persiųsti išimtį į aukštesnį lygį
        }
    }

    // Papildomai įtraukite Decrypt metodą, jei jis dar nėra
}
