
"""run.py  runs the entire system, calling the parser and
   then the interpreter
"""
import parse
import interpreter
tree = parse.parse()
print
print tree
print
interpreter.interpretPTREE(tree)
