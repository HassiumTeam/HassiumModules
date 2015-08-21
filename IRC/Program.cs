using System;
using System.Collections.Generic;
using Hassium;

namespace IRCLib
{
	public class Program : ILibrary
	{
		public Dictionary<string, InternalFunction> GetFunctions()
		{
			Dictionary<string, InternalFunction> result = new Dictionary<string, InternalFunction>();
			result.Add("newirc", new InternalFunction(Program.NewIRC));
	            	result.Add("ircconnect", new InternalFunction(Program.IRCConnect));
        	    	result.Add("ircjoin", new InternalFunction(Program.IRCJoin));
	            	result.Add("ircget", new InternalFunction(Program.IRCGet));
        	    	result.Add("ircsend", new InternalFunction(Program.IRCSend));
	            	result.Add("ircmsgmsg", new InternalFunction(Program.IRCMsgMsg));
        	    	result.Add("ircmsgchan", new InternalFunction(Program.IRCMsgChan));
	            	result.Add("ircmsgsender", new InternalFunction(Program.IRCMsgSender));

			return result;
		}

	        public static object NewIRC(object[] args)
            	{
                	return new IRC(args[0].ToString(), Convert.ToInt32(args[1]), args[2].ToString(), args[3].ToString());
            	}

            	public static object IRCConnect(object[] args)
            	{
                	((IRC)(args[0])).Connect();
                	return null;
            	}

            	public static object IRCJoin(object[] args)
            	{
                	((IRC)(args[0])).Join(args[0].ToString());
                	return null;
            	}

            	public static object IRCGet(object[] args)
            	{
                	return((IRC)(args[0])).GetMessage();
            	}

            	public static object IRCSend(object[] args)
            	{
                	((IRC)(args[0])).SendRaw(arrayToString(args, 1));
                	return null;
            	}

            	public static object IRCMsgMsg(object[] args)
            	{
                	return ((IRCMessage)(args[0])).Message;
            	}

            	public static object IRCMsgChan(object[] args)
            	{
                	return ((IRCMessage)(args[0])).Channel;
            	}

            	public static object IRCMsgSender(object[] args)
            	{
                	return ((IRCMessage)(args[0])).Sender;
            	}

            	private static string arrayToString(object[] args, int startIndex = 0)
            	{
                	string result = "";

                	for (int x = startIndex; x < args.Length; x++)
                    		result += args[x].ToString();

                	return result;
		}
	}
}
