using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OOP_Eksamen
{
    internal class StregsystemController
    {
        private IStregsystemUI _ui;
        private IStregsystem _stregsystem;

        private Dictionary<string, Action<string[]>> _adminCommands = new Dictionary<string, Action<string[]>>();


        /// <summary>
        /// Handles the stregsystem.
        /// </summary>
        /// <param name="ui">A IStregsystemUI type.</param>
        /// <param name="stregsystem">A IStregsystem type.</param>
        public StregsystemController(IStregsystemUI ui, IStregsystem stregsystem)
        {
            _ui = ui;
            _stregsystem = stregsystem;
            _stregsystem.UserBalanceWarning += _ui.DisplayBalanceWarning;
            ui.CommandEvent += ParseCommand;
            _stregsystem.InputUsersFromFile();
            _stregsystem.InputProductFromFile();

            _adminCommands.Add(":quit", (s) => _ui.Close());
            _adminCommands.Add(":activate", (s) =>
            {
                _stregsystem.GetProductByID(Int32.Parse(s[1])).Active = true;
                _ui.DisplayProducts();
            });
            _adminCommands.Add(":deactivate", (s) =>
            {
                _stregsystem.GetProductByID(Int32.Parse(s[1])).Active = false;
                _ui.DisplayProducts();
            });
            _adminCommands.Add(":crediton", (s) => _stregsystem.GetProductByID(Int32.Parse(s[1])).CanBeBoughtOnCredit = true);
            _adminCommands.Add(":creditoff", (s) => _stregsystem.GetProductByID(Int32.Parse(s[1])).CanBeBoughtOnCredit = false);
            _adminCommands.Add(":addcredits", (s) => _stregsystem.AddCreditsToAccount(_stregsystem.GetUserByUsername(s[1]), Decimal.Parse(s[2])));
        }
        /// <summary>
        /// The different commands that CommandEvent method calls
        /// </summary>
        /// <param name="command"></param>
        public void ParseCommand(string command)
        {
            try
            {
                string[] commandParts = command.Split(' ');
                Action<string[]> outPut;
                if (_adminCommands.TryGetValue(commandParts[0], out outPut))
                {
                    outPut.Invoke(commandParts);
                }
                else
                {
                    if (commandParts.Length == 1 && !commandParts[0].Contains(":"))
                    {
                        User user = _stregsystem.GetUserByUsername(commandParts[0]);
                        _ui.DisplayUserInfo(user);
                    }

                    else if (commandParts.Length == 2)
                    {
                        User user = _stregsystem.GetUserByUsername(commandParts[0]);
                        Product product = _stregsystem.GetProductByID(Int32.Parse(commandParts[1]));
                        BuyTransaction transaction = _stregsystem.BuyProduct(user, product);
                        _ui.DisplayUserBuysProduct(transaction);
                    }
                    else if (commandParts.Length == 3)
                    {
                        int count = Int32.Parse(commandParts[2]);
                        if (count > 0)
                        {
                            User user = _stregsystem.GetUserByUsername(commandParts[0]);
                            Product product = _stregsystem.GetProductByID(Int32.Parse(commandParts[1]));
                            List<BuyTransaction> transactions = new List<BuyTransaction>();
                            for (int i = 0; i < count; i++)
                            {
                                BuyTransaction transaction = _stregsystem.BuyProduct(user, product);
                                transactions.Add(transaction);
                            }
                            _ui.DisplayUserBuysProduct(count, transactions);
                        }
                        else
                        {
                            _ui.DisplayGeneralError("You can't buy 0 times");
                        }
                    }
                    else if (commandParts.Length > 3)
                    {
                        _ui.DisplayTooManyArgumentsError(string.Concat(commandParts.Aggregate((prev, current) => prev + " " + current)));
                    }
                    else
                    {
                        _ui.DisplayAdminCommandNotFoundMessage(commandParts[0]);
                    }
                }
            }
            catch (UserNotFoundException e)
            {
                _ui.DisplayUserNotFound(e.Message);
            }
            catch (ProductNotFoundException e)
            {
                _ui.DisplayProductNotFound(e.id);
            }
            catch (ArgumentNullException e)
            {
                _ui.DisplayGeneralError(e.Message);
            }
            catch (EmailNotLegalException e)
            {
                _ui.DisplayGeneralError($"Email not legal: {e.email}");
            }
            catch (InsufficientCreditsException e)
            {
                _ui.DisplayInsufficientCash(e.user, e.product);
            }
            catch (UsernameNotLegalException e)
            {
                _ui.DisplayGeneralError($"Ilegal username: {e.username}");
            }
            catch (ProductNotActiveException e)
            {
                _ui.DisplayGeneralError(e.Message);
            }
            catch (IdException e)
            {
                _ui.DisplayGeneralError("The Id of can't be: " + e.id.ToString());
            }
            catch (NullReferenceException e)
            {
                _ui.DisplayGeneralError(e.Message);
            }
        }
    }
}