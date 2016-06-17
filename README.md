# TeleTurk
* This project is driven from https://github.com/sochix/TLSharp and thanks to Sochix.
* Thanks to @XyLoNaMiyX for his amazing AutoCompiler code : https://github.com/LonamiWebs/TLSharp.CodeGenerator

# Status
Do not try to use this project!!! it is still under development and needs a lot of things to implement!

# How To Use
1. Create a [developer account](https://my.telegram.org/) in Telegram.
2. Fill the [Configuration.cs] (https://github.com/ShanalTeam/TeleTurk/blob/master/TeleTurk.Core/Configuration.cs) with apiHash and apiID you got from developer account, you also need to put your registered phonenumber there. 

## Authentication

```
var store = new FileSessionStore();                     // store session file
var client = new TelegramClient(store, "session");      // create client
await client.Connect();                                 // try to connect
var hash = await client.SendCodeRequest();              // send Code request for registerd phone number
var code = "87493";                                     // you can change code in debugger
var user = await client.MakeAuth(hash, code);           // send code and hash to get confirm from telegram
```

## Using the Telegram types
You have different ways to use the Telegram types. One is casting the `abstract` type to its real type. For example:
`Message message = ...; // we don't know what type it is, we want the message id`
`int id;`
`if (message is TL.MessageType)`
`	id = ((TL.MessageType)message).Id;`
`else if (message is TL.MessageType)`
`	id = ((TL.MessageServiceType)message).Id;`
`...`

This however can quickly become annoying. You can also do:
`Message message = ...;`
`int id = (int)message["Id"];`

Fairly easier!

# Donations
Thanks for donations! It's highly appreciated. 
Bitcoin wallet: **3K1ocweFgaHnAibJ3n6hX7RNZWFTFcJjUe**

List of donators:
* [mtbitcoin](https://github.com/mtbitcoin)

# License

**Please, provide link to an author when you using library**

The MIT License

* Copyright (c) 2015 Ilya Pirozhenko http://www.sochix.ru/
* Copyright (c) 2016 Lonami totufals@hotmail.com
* Copyright (c) 2016 Ehsan Hesam ehsan.hesam13@gmail.com

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
