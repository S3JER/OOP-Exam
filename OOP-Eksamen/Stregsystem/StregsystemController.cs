using System;
using System.Collections.Generic;

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
                if (commandParts[0].Contains(":"))
                {
                    if (_adminCommands.TryGetValue(commandParts[0], out outPut))
                    {
                        outPut.Invoke(commandParts);
                    }
                    else
                    {
                        throw new AdminCommandDoNotExistExecption(commandParts[0]);
                    }
                }
                else
                {
                    switch (commandParts.Length)
                    {
                        case 1:
                            User user1 = _stregsystem.GetUserByUsername(commandParts[0]);
                            _ui.DisplayUserInfo(user1);
                            break;
                        case 2:
                            User user2 = _stregsystem.GetUserByUsername(commandParts[0]);
                            Product product2 = _stregsystem.GetProductByID(ValidateString(commandParts[1]));
                            BuyTransaction transaction2 = _stregsystem.BuyProduct(user2, product2);
                            _ui.DisplayUserBuysProduct(transaction2);
                            break;
                        case 3:
                            int count = ValidateString(commandParts[2]);
                            if (count > 0)
                            {
                                User user3 = _stregsystem.GetUserByUsername(commandParts[0]);
                                Product product3 = _stregsystem.GetProductByID(ValidateString(commandParts[1]));
                                List<BuyTransaction> transactions3 = new List<BuyTransaction>();
                                for (int i = 0; i < count; i++)
                                {
                                    BuyTransaction transaction3 = _stregsystem.BuyProduct(user3, product3);
                                    transactions3.Add(transaction3);
                                }
                                _ui.DisplayUserBuysProduct(count, transactions3);
                            }
                            else
                            {
                                _ui.DisplayGeneralError("You can't buy 0 times");
                            }
                            break;
                        default:
                            throw new TooManyArgumentsException(command);
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
            catch (TooManyArgumentsException e)
            {
                _ui.DisplayTooManyArgumentsError(e.Message);
            }
            catch (AdminCommandDoNotExistExecption e)
            {
                _ui.DisplayAdminCommandNotFoundMessage(e.Message);
            }
            //catch (FormatException)
            //{
            //    _ui.DisplayGeneralError("Something went wrong, try again");
            //}
        }
        private int ValidateString(string value)
        {
            if (value != null && value != "")
            {
                string usableChars = "0123456789";
                foreach (char c in value)
                {
                    if (!usableChars.Contains(c))
                    {
                        throw new ProductNotFoundException(value);
                    }
                }
                return Int32.Parse(value);
            }
            throw new ArgumentNullException();
        }
    }
}