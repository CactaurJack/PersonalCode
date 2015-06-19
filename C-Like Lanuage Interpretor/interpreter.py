
"""Interpreter for Assignment 3 language with type structures and procedures,
   where storage layout is Java-like.
"""

### HEAP:

heap = {}
heap_count = 0  # how many objects stored in the heap

"""The program's heap --- a dictionary that maps heaplocs to objects;
     the objects can be arrays or structs

      heap : { (heaploc : STORABLE)+ }
             where  STORABLE = [ DENOTABLE + ]  |   { (ID : DENOTABLE)* }
                    DENOTABLE = int | heaploc | CLIST
                      int = an integer
                      heaploc = string of digits, denoting a heap location
                      CLIST = a command-list operator tree (a procedure body)

   Example:
     heap = { "0": {"x": 7, "y":"1", "z": "nil", "p": [["=" ["x"], "7"]]},
              "1": ["2", "nil", 0], 
              "2": {"r": 99} }
     heap_count = 3
        is an example heap, where heaploc "0" holds a struct
        whose  x  field holds int 7, "y" field holds heaploc "1"
        (link to the object at heaploc "1"), "z" holds nil (uninitialized), 
        and "p" holds a one-command CLIST ("x = 7").
       Heaploc "1" holds a three-celled
        array whose elements are heaploc "2", "nil", and int 0;
       Heaploc "2" holds a struct with a single field, "r"
"""

### NAMESPACE (environment):

ns = ""   # This will be initialized in the main function, interpretPTREE, as follows:
          #    ns = "0"
          #    heap = {"0": {}}
          #    heap_count = 1

"""The program's namepsace, ns, is a link to a dictionary saved in
   the heap, like it is done in Java and Python.  The above example
   heap was generated from this sample program:
       var x; var y; var z;
       proc p: x = 7 end;
       y = new array[3];
       y[2] = 0;
       y[0] = new struct r end;
       y[y[2]].r = 99;
       call p
"""

### ASSOCIATED MAINTENANCE FUNCTIONS FOR THE  heap:

def reserveNewLocation():
    """allocates a new location in the heap and
       returns the  location
    """
    global heap_count
    newloc = str(heap_count)
    heap_count = heap_count + 1
    return newloc


def dereference(lval) :
    """looks up the value of  lvalue  in the heap
       param: lval is a pair,  (heaploc,qualifier),
        where  heaploc is the address of an object in the heap
               and  qualifier is the _offset_ (index) into the ob,
               either an int (in the case that heaploc points to an array)
               or an ID (when heaploc points to a dictionary).

       returns: Say that  lval = (h,i).  The function extracts the
               object at  heap[h], indexes it with i,  and returns
               the value found there:  return  heap[h][i]
               For example, for the heap at the top of this program,
                 dereference(("0","x")) returns 7
                 dereference(("1",2)) returns "nil"
                 dereference(("0","p")) returns [["=" ["f"], "7"]]
    """
    return heap[lval[0]][lval[1]]


def store(lval, rval) :
    """stores the  rval  into the heap at  lval
       params:  lval -- a pair,  (heaploc, index), as described above
                rval -- an int or a heaploc

       Say that  lval = (h,i).  The function finds the object at heaploc h
         and saves  rval  at index  i  within that object:  heap[h][i] = rval.
       For example, for the heap at the top of this program,
                 store(("1",1), 98)  would replace the "nil" in the
       array at heaploc "1" by 98.
    """
    heap[lval[0]][lval[1]] = rval
    


##########################################################################
### Here is the syntax of operator trees interpreted by this interpreter:

"""
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

#########################################################################
"""

# See the end of program for the driver function,  interpretPTREE

def interpretDLIST(dlist) :
    """pre: dlist  is a lists of DECLS:   DLIST ::=  [ DTREE+ ]
       post:  ns  holds the declarations in  dlist
    """
    for dtree in dlist :
        interpretDTREE(dtree)

def interpretDTREE(d) :
    """pre: d  is a   DTREE ::=  ["var", ID]  |  ["proc", ID, CLIST]
         CTREE ::= ["=", VAR, ETREE] | ["print", VAR] | ["while", ETREE, CLIST]
       post:  ns  holds the declration, provided that the  ID  is not in use
    """
    
    if (d[0] == "var") :
        vars[d[1]] = "nil"
    elif (d[0] == "proc") :
        vars[d[1]] = d[2]
    #Done V 1.5.3


def interpretCLIST(clist) :
    """pre: clist  is a program represented as a  CLIST ::=  [ CTREE+ ]
                  where  CTREE+  means  one or more CTREEs
       post:  memory  holds all the updates commanded by program  p
    """
    for command in clist :
        interpretCTREE(command)


def interpretCTREE(c) :
    """pre: c  is a command represented as a CTREE:
       CTREE ::=  ["=", LTREE, ETREE]  |  ["if", ETREE, CLIST, CLIST2] 
        |  ["print", LTREE]  |  ["call", ID]
       post:  heap  holds the updates commanded by  c
    """
    Chold = 0
    if(c[0] == "=") :
        Ctemp = interpretLTREE(c[1])
        TempD = interpretETREE(c[2])
        if(str(Ctemp[1][0]) == "+" or str(Ctemp[1][0]) == "-") :
            print Ctemp[1]
            Ctemp[1] = str(interpretETREE(Ctemp[1]))
        temp = str(TempD)
        if (temp.isdigit()) :
            store(Ctemp, TempD)
        else :
            store(Ctemp, str(heap_count - 1))
    if(c[0] == "if") :
        heap[Chold]
    elif(c[0] == "print") :
        PTemp = interpretLTREE(c[1])
        print heap[PTemp[0]][PTemp[1]]
    elif(c[0] == "call") :
        interpretCLIST(vars[c[1]])
    


def interpretETREE(etree) :
    """interpretETREE computes the meaning of an expression operator tree.
         ETREE ::=  NUM  |  ["+", ETREE, ETREE] |  ["deref", LTREE] 
                 |  ["new", TTREE ]
        post: returns the  etree's value, either an int, a heaploc, or "nil"
    """
    ans = "nil"
    temp = str(etree)
    if isinstance(etree, str) and temp.isdigit() :  # NUM
      ans = int(etree) 
    elif  etree[0] == "+" :                        # ["+", ETREE, ETREE]
        ans1 = interpretETREE(etree[1])
        ans2 = interpretETREE(etree[2])
        if isinstance(ans1,int) and isinstance(ans2, int) :
            ans = ans1 + ans2
        else : crash("addition error --- nonint value used")
    elif  etree[0] == "deref" :
        return dereference(interpretLTREE(etree[1]))
    elif  etree[0] == "new" :
        ns = reserveNewLocation()
        heap[ns] = interpretTTREE(etree[1])
        return ans
    else :  crash("invalid expression form")
    return ans

def interpretTTREE(tree) :
    """interpretTTREE computes the meaning of a TTREE:
           TTREE ::=  ["array", N]  |  ["struct", FLIST]
       This will allocate a new location in the heap and update the
       heap so that it holds the indicated object.  

       Returns the  heaploc  where the object was saved in  heap
         For ["array", N], a list of N "nil"s will be saved at  heap[heaploc]
         For ["struct", [id1,id2, ...]], a dictionary of form
          {id1:"nil", id2:"nil",...} will be saved at  heap[heaploc]
    """
    Ttemp = {}
    if(tree[0] == "array") :
        for i in range(0, int(tree[1])) :
            Ttemp[str(i)] = "nil"
    
    if(tree[0] == "struct") :
        for i in range(0, int(len(tree[1]))) :
            Ttemp[str(tree[1][i])] = "nil"
        return Ttemp
        
    return Ttemp


def interpretLTREE(ltree) :
    """interpretLTREE computes the meaning of a lefthandside operator tree.
          LTREE ::=  [ID,  REF* ]     where  *  means "zero or more"
          REF ::=  ["index", ETREE]  |  ["dot", ID]
       Here are some example  ltree values:
         ["x"]                                  #  x
         ["y", ["index", "0"]]                  #  y[0]
         ["y", ["index", "0"], ["dot", "f"]]    #  y[0].f
         ['y', ['index', ['deref', ['y', ['index', '2']]]]]   #  y[y[2]]

       Returns a pair, (heaploc, qualifier), where  heaploc is the location
         in the heap where an object is stored, and  qualifier is an int or
         ID that indexes into the object to where a DENOTABLE is saved.

       For example, look again at the example heap at the very top of
         this program.  For that heap, here is what the function returns:
          ["x"]                                  #  ("0","x")
          ["y", ["index", "0"]]                  #  ("1", 0)
          ["y", ["index", "0"], ["dot", "f"]]    #  ("2","f")
          ['y', ['index', ['deref', ['y', ['index', '2']]]]]  # ("1",2)
    """
    Ltemp = "nil"
    Ltemp2 = "nil"
    Dtemp = "nil"
    Ltemp = "nil"
    if (len(ltree) == 1) :
        Ltemp = ["0",ltree[0]]
        return Ltemp
    if (ltree[1][0] == "index") :
        if( 2 == len(ltree) - 1 and ltree[2][0] == "index" ) :
            Ltemp = [ltree[0], ltree[1]]
            Dtemp = interpretLTREE(Ltemp)
            Ltemp2 = [Dtemp[1], ltree[2]]
            Dtemp = interpretLTREE(Ltemp2)
            return Dtemp
        
        Ltemp = ["nil","nil"]   
        Ltemp[1] = ltree[1][1]
        if (ltree[0].isdigit()) :
            Ltemp[0] = ltree[0]
            return Ltemp
        else :
            Ltemp[0] = heap["0"][ltree[0]]
            return Ltemp
    
    if (ltree[1][0] == "dot") :
        Ltemp = ["nil","nil"]
        Ltemp[1] = ltree[1][1]
        if (ltree[0].isdigit()) :
            Ltemp[0] = ltree[0]
        else :
            Ltemp[0] = heap["0"][ltree[0]]
        return Ltemp
            
        
            
    


###########################

def crash(message) :
    """pre: message is a string
       post: message is printed and interpreter stopped
    """
    print message + "! crash! ns=", ns, "heap=", heap
    raise Exception   # stops the interpreter


# MAIN FUNCTION ###########################################################

def interpretPTREE(tree) :
    """interprets a complete program tree
       pre: tree is a  PTREE ::= [ DLIST, CLIST ]
       post: final values are deposited in memory and env
    """
    global ns, heap, Dloc, vars, LTree, CTemp
    # initialize heap and ns:
    heap = {}
    ns = reserveNewLocation()
    vars = {}
    heap[ns] = vars 
    LTree = 0
    CTemp = 0;
    

    interpretDLIST(tree[0])
    interpretCLIST(tree[1])
    print "final namespace =", ns
    print "final heap =", heap

    raw_input("Press Enter key to terminate")
