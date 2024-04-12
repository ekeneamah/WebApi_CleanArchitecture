using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.Content.Services
{
    public class TRansactionComplete
    {
        private readonly Transaction _transaction;

        public TRansactionComplete() { 
        }

        public string GetEmailBody(Transaction transaction,Product p, string FirstName )
        {

            // Replace placeholders in the HTML template with actual values from the Transaction object
            string emailBody = $@"
        <html>
        <head>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    background-color: #f4f4f4;
                    color: #333;
                    margin: 0;
                    padding: 0;
                }}
                .container {{
                    width: 600px;
                    margin: 20px auto;
                    padding: 20px;
                    background-color: #fff;
                    border-radius: 5px;
                    box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
                }}
                h2 {{
                    color: #007bff;
                }}
                p {{
                    margin-bottom: 10px;
                }}
                ul {{
                    list-style-type: none;
                    padding: 0;
                }}
                ul li {{
                    margin-bottom: 5px;
                }}
            </style>
        </head>
        <body>
            <div class='container'>
                <h2>Transaction Confirmation</h2>
                <p>Hello {FirstName},</p>
                <p>Your transaction with reference number <strong>{transaction.Reference}</strong> has been processed successfully.</p>
                <p>Transaction Details:</p>
                <ul>
                    <li>Amount: {transaction?.Amount}</li>
                    <li>Date: {transaction?.DateTime}</li>
                    <li>Stattus: {transaction?.Status}</li>
                    <li>Product: {p.Product_Name} </li>
                   
                </ul>
                <p>Thank you for choosing our services!</p>
            </div>
        </body>
        </html>";

            // Return the populated HTML content
            return emailBody;
        }

        public string GetEmailBodyInsuranceCoy(Transaction transaction, Product p, string FirstName)
        {

            // Replace placeholders in the HTML template with actual values from the Transaction object
            string emailBody = $@"
        <html>
        <head>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    background-color: #f4f4f4;
                    color: #333;
                    margin: 0;
                    padding: 0;
                }}
                .container {{
                    width: 600px;
                    margin: 20px auto;
                    padding: 20px;
                    background-color: #fff;
                    border-radius: 5px;
                    box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
                }}
                h2 {{
                    color: #007bff;
                }}
                p {{
                    margin-bottom: 10px;
                }}
                ul {{
                    list-style-type: none;
                    padding: 0;
                }}
                ul li {{
                    margin-bottom: 5px;
                }}
            </style>
        </head>
        <body>
            <div class='container'>
                <h2>Transaction Confirmation</h2>
                <p>Hello ,</p>
                <p>A transaction with reference number <strong>{transaction.Reference}</strong> has been processed successfully.</p>
                <p>Transaction Details:</p>
                <ul>
                    <li>Amount: {transaction.Amount}</li>
                    <li>Date: {transaction.DateTime}</li>
                    <li>Stattus: {transaction.Status}</li>
                    <li>Product: {p.Product_Name} </li>
<li>Payment Ref.: {transaction.PaymentRef} </li>

                   
                </ul>
                <p>Thank you for choosing our services!</p>
            </div>
        </body>
        </html>";

            // Return the populated HTML content
            return emailBody;
        }

    }
}
