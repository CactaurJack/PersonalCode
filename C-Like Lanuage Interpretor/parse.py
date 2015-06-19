
"""A module of functions that reformat an input program into an
   operator tree.   Call  parse()  to run the parser.

P : Program
D : Declaration
C : Command
E : Expression
T : TypeStructure
F : Field
L : LefthandSide
I : Identifier
N : Numeral

P ::=  D ; C

D ::=  D1 ; D2  |  var I  |  proc I : C end

C ::=  C1 ; C2  |  L = E  |  if E : C1 else C2 end  |  print L  |  call I

E ::=  N  |  ( E1 + E2 )  |  L  |  new T

T ::=  array [ N ]  |  struct F end  

F ::=  I  |  F1 , F2

L ::=  I  |  L [ E ]  |  L . I

N ::=  strings of digits

I ::=  strings of letters, not including keywords

The output operator trees are nested lists:

PTREE ::=  [ DLIST, CLIST ]

DLIST ::=  [ DTREE+ ]
           where  DTREE+  means  one or more DTREEs

DTREE ::=  ["var", ID]  |  ["proc", ID, CLIST]

CLIST ::=  [ CTREE+ ]
           where  CTREE+  means  one or more CTREEs

CTREE ::=  ["=", LTREE, ETREE]  |  ["if", ETREE, CLIST, CLIST2] 
        |  ["print", LTREE]  |  ["call", ID]

ETREE ::=  NUM  |  ["+", ETREE, ETREE] |  ["deref", LTREE]  |  ["new", TTREE ]

TTREE ::=  ["array", N]  |  ["struct", FLIST]

FLIST ::=  [ ID+ ]

LTREE ::=  [ID,  REF* ]
           where  REF* means 0 or more REFs

REF ::=  ["index", ETREE]  |  ["dot", ID]

NUM   ::=  a nonempty string of digits

ID    ::=  a nonempty string of letters
"""

### data structures for parser:
wordlist = []  # holds the remaining unread words
nextword = ""  # holds the first unread word
# global invariant:  nextword + wordlist == all remaining unread words
EOF = "!"      # a word that marks the end of the input words

def getNextword() :
    """moves the front word in  wordlist  to  nextword.
       If wordlist is empty, sets  nextword = EOF
    """
    global nextword, wordlist
    if wordlist == [] :
        nextword = EOF
    else :
        nextword = wordlist[0]
        wordlist = wordlist[1:]

#####

def error(message) :
    """prints an error message and halts the parse"""
    print "parse error: " + message
    print nextword, wordlist
    raise Exception


def isVar(word) :
    """checks whether  word  is a legal variable name"""
    KEYWORDS = ("var", "proc", "print", "if", "else", "call", "end", "new", "int", "array", "of", "struct")
    ans = ( word.isalpha()  and  not(word in KEYWORDS) )
    return ans


def parseTypestructure() :
    """builds a TTREE.
       input:  T ::=  array [ N ] |  struct F end 
               F ::=  I : T  |  F1 , F2
       output:  TTREE ::=  ["array", N, TTREE]  |  ["struct", ID+]
    """
    ans = []
    if nextword == "array" :   # array [N]
        getNextword()
        if nextword == "[" :
            getNextword()
        else : error("missing [ in array decl")
        if  nextword.isdigit() :   # N
            arraylth = nextword
            getNextword()
        else : error("missing length of array")
        if nextword == "]" :
            getNextword()
        else : error("missing ] of array decl")
        ans = ["array", arraylth]
    elif nextword == "struct" :  # struct F end
        getNextword()
        fieldlist = []
        while nextword != "end" :
            if isVar(nextword) :
                fieldlist.append(nextword)
                getNextword()
            else : error("missing fieldname in struct defn")
            if nextword == "," :
                getNextword()
        getNextword()  # consume  "end"
        ans = ["struct", fieldlist]
    else : error("invalid type structure")
    return ans 
            

def parseLefthandside() :
    """builds an LTREE
       input:  L ::=  I  |  L [ E ]  |  L . I
         treated as   L ::=  I TAIL
                      TAIL ::=  [ E ] TAIL  |  . I TAIL  |  (nothing more)
       output: LTREE ::=  [ ID, REF* ]
               REF ::=  ["index", ETREE]  |  ["dot", ID]
    """
    ans = []
    if isVar(nextword) :   # I
        ans = [nextword]
        getNextword()
    else : error("invalid left-hand side")
    while nextword in ("[", ".") :
        if nextword == "[" :
            getNextword()
            indexval = parseEXPR()
            if nextword == "]" :
                getNextword()
                ans.append(["index", indexval])
            else : error("missing ]")
        else :  # nextword == "." 
            getNextword()
            if isVar(nextword) :
                ans.append(["dot", nextword])
                getNextword()
            else : error("missing fieldname")
    return ans
    
    

def parseEXPR() :
    """builds an EXPR operator tree
       input: E ::=  N  |  ( E1 + E2 )  |  L  |  new T
       output: ETREE ::=  NUM  |  ["+", ETREE, ETREE] |  ["deref", LTREE] 
                       |  ["new", TTREE ]
    """
    ans = []
    if  nextword.isdigit() :   # N
        ans = nextword        
        getNextword()
    elif  isVar(nextword) :    # L
        ans = ["deref", parseLefthandside()]
    elif nextword == "new" :   # new T
        getNextword()
        ans = ["new", parseTypestructure() ]
    elif nextword == "(" :     # ( EXPR + EXPR ) 
        getNextword()
        tree1 = parseEXPR()
        op = nextword
        if op == "+" :
            getNextword()
            tree2 = parseEXPR()
            if nextword == ")" :
                ans = [op, tree1, tree2]
                getNextword()
            else :
                error("missing )")
        else :
            error("missing operator")
    else :
        error("illegal symbol to start an expression")
    return ans


def parseCOMMAND() :
    """builds a COMMAND operator tree 
       input: C ::=  L = E  |  if E : C1 else C2 end  |  print L  |  call I
       output: 
        CTREE ::=  ["=", LTREE, ETREE]  |  ["if", ETREE, CLIST, CLIST2] 
                |  ["print", LTREE]  |  ["call", ID]
    """
    #Parser would always through errors without this, don't really know why.
    ans = []
    if nextword == "print" :      # print L
        getNextword()
        tree = parseLefthandside()
        ans = ["print", tree]
    elif nextword == "if" :    # if E : C1 else C2
        getNextword()
        exprtree = parseEXPR()
        if nextword == ":" :
            getNextword()
        else : error("missing :")
        thentree = parseCMDLIST()
        if nextword == "else" :
            getNextword()
        else : error("missing else")
        elsetree = parseCMDLIST()
        if nextword == "end" :
            ans = ["if", exprtree, thentree, elsetree]
            getNextword()
        else :
            error("missing end")
    elif nextword == "call" :   # call I
        getNextword()
        if isVar(nextword) :
            id = nextword
            ans = ["call", id]
            getNextword()
        else : error("missing name of called proc")
    elif isVar(nextword) :       # L = E
        ltree = parseLefthandside()
        if nextword == "=" :
            getNextword()
            exprtree = parseEXPR()
            ans = ["=", ltree, exprtree]
        else :
            error("missing =")
    else :                       # error -- bad command
       
        error("bad word to start a command")
    return ans


def parseCMDLIST() :
    """builds a CMDLIST tree
       input:  C ::=  C1 ; C2 | ...
       output:  CLIST ::= [ CTREE+ ]
    """
    anslist = [ parseCOMMAND() ]   # parse first command
    while nextword == ";" :        # collect any other COMMANDS
        getNextword()
        #Parser would always through errors without this, don't really know why.
        if(nextword == "!") :
            return anslist
            
        anslist.append( parseCOMMAND() )
    return anslist


def parseDECL() :
    """builds a DECL operator tree
       input: D ::=  var I  |  proc I = C end
       output:  DTREE ::=  ["var", ID]  |  ["proc", ID, CLIST]
    """
    ans = []
    if nextword == "var" :      # var I
        getNextword()
        if isVar(nextword) :
            ans = ["var", nextword]
            getNextword()
        else :
            error("expected var")
    elif nextword == "proc" :    # proc I = C end 
        getNextword()
        if isVar(nextword) :
            pname = nextword
            getNextword()
            if nextword == ":" :
               getNextword()
               body = parseCMDLIST()
               if nextword == "end" :
                   ans = ["proc", pname, body]
                   getNextword()
               else : error("missing end of proc decl")
            else : error("missing = of proc decl")
        else : error("missing name of proc")
    else : error("invalid declaration")
    return ans


def parseDECLIST():
    """builds a DECLIST operator tree
       input:  D ::=  D1 ; D2 | ...
       output: DLIST ::=  [ DTREE+ ]
    """
    ans = []
    while nextword in ("var", "proc") :
        ans.append(parseDECL())
        if nextword == ";" :
            getNextword()
        else : error("missing semicolon after declaration")
    return ans
       

def parse() :
    """reads the input program and builds an operator tree for it,

       input: the program to be analyzed, entered from the console as a string
              P ::=  D ; C
       output: the operator tree,  PTREE ::=  [ DLIST, CLIST ]
    """
    global wordlist
    import scanner   # import and link to scanner module
    print "Type program; OK to do it on multiple lines; terminate with  !"
    print "  as the first symbol on a line by itself:"
    print
    text = ""
    line = raw_input("" )
    while line[0] != "!" :
        text = text + " " + line
        line = raw_input("" )
    
    print text
    wordlist = scanner.scan(text)   # initialize parser with program's words
    print wordlist
    getNextword()
    # assert: invariant for nextword and wordlist holds true here
    dtree = parseDECLIST()
    ctree = parseCMDLIST()   
    tree =[dtree, ctree]
    # assert: tree holds the entire operator tree for  text
    #print tree
    if nextword != EOF :
       error("there are extra words")
    return tree
