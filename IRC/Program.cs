using System;
using System.Collections.Generic;
using Hassium;

namespace IRCLib
{
	public class Program : ILibrary
	{
				[IntFunc("newirc")]
				public static object NewIRC(object[] args)
            	{
                	return new IRC(args[0].ToString(), Convert.ToInt32(args[1]), args[2].ToString(), args[3].ToString());
            	}
				[IntFunc("ircconnect")]
            	public static object IRCConnect(object[] args)
            	{
                	((IRC)(args[0])).Connect();
                	return null;
            	}
				[IntFunc("ircjoin")]
            	public static object IRCJoin(object[] args)
            	{
                	((IRC)(args[0])).Join(args[0].ToString());
                	return null;
            	}
				[IntFunc("ircget")]
            	public static object IRCGet(object[] args)
            	{
                	return((IRC)(args[0])).GetMessage();
            	}
				[IntFunc("ircsend")]
            	public static object IRCSend(object[] args)
            	{
                	((IRC)(args[0])).SendRaw(arrayToString(args, 1));
                	return null;
            	}
				[IntFunc("ircmsgmsg")]
            	public static object IRCMsgMsg(object[] args)
            	{
                	return ((IRCMessage)(args[0])).Message;
            	}
				[IntFunc("ircmsgchan")]
            	public static object IRCMsgChan(object[] args)
            	{
                	return ((IRCMessage)(args[0])).Channel;
            	}
				[IntFunc("ircmsgsender")]
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
