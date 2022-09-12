using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_server
{
    internal class Chatbot
    {
        /*
        <message>       ::= ['@' <tags> <SPACE>] [':' <prefix> <SPACE> ] <command> [params] <crlf>
        <tags>          ::= <tag> [';' <tag>]*
        <tag>           ::= <key> ['=' <escaped_value>]
        <key>           ::= [ <client_prefix> ] [ <vendor> '/' ] <key_name>
        <client_prefix> ::= '+'
        <key_name>      ::= <non-empty sequence of ascii letters, digits, hyphens ('-')>
        <escaped_value> ::= <sequence of zero or more utf8 characters except NUL, CR, LF, semicolon (`;`) and SPACE>
        <vendor>        ::= <host> 
         */
    }
}
