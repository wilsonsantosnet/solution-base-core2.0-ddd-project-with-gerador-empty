using Common.Domain.Model;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.OpenSsl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Common.Payment.Pagarme
{
    public static class CardHash
    {
        /// <summary>
        /// https://github.com/pagarme/pagarme-pocs/blob/master/dotNet%20-%20POCs/PagarMe.Net.PoCs/Entities/CardHashMaker.cs
        /// </summary>
        /// <param name="id"></param>
        /// <param name="publicKey"></param>
        /// <param name="card_number"></param>
        /// <param name="card_expiration_date"></param>
        /// <param name="card_holder_name"></param>
        /// <param name="card_cvv"></param>
        /// <returns></returns>
        public static string Make(string id,string publicKey, CreditCard creditCard)
        {

            var args = new List<Tuple<string, string>>();

            args.Add(new Tuple<string, string>("card_number", creditCard.Number));
            args.Add(new Tuple<string, string>("card_expiration_date", creditCard.ExpireMonth + creditCard.ExpireYear.Substring(creditCard.ExpireYear.Length - 2)));
            args.Add(new Tuple<string, string>("card_holder_name", creditCard.FirstName.Replace(" ", "%20") + " % 20" + creditCard.LastName.Replace(" ", "%20")));
            args.Add(new Tuple<string, string>("card_cvv", creditCard.Cvv));

            var data = Encoding.UTF8.GetBytes(args.Select((t) => Uri.EscapeUriString(t.Item1) + "=" + Uri.EscapeUriString(t.Item2)).Aggregate((c, n) => c + "&" + n));
            var result = "";

            using (var reader = new StringReader(publicKey))
            {
                var pemReader = new PemReader(reader);
                var key = (AsymmetricKeyParameter)pemReader.ReadObject();
                var rsa = new Pkcs1Encoding(new RsaEngine());

                rsa.Init(true, key);

                result = id + "_" + Convert.ToBase64String(rsa.ProcessBlock(data, 0, data.Length));

            }

            return result;

        }

    }
}
