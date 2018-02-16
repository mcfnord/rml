import sys
import os.path
import time
from HTMLParser import HTMLParser

gfImporting = False
gstrClassname = None
gInsertSought = False

def S3BinaryHostReplace( str ):
    str = str.replace("(img/", "(http://b00p.s3-us-west-2.amazonaws.com/img/")
    str = str.replace("(doc/", "(http://b00p.s3-us-west-2.amazonaws.com/doc/")
    return str ;

class MyHTMLParser(HTMLParser):
    def handle_starttag(self, tag, attrs):
        global gfImporting
        global gstrClassname
        if tag == 'div': # is apparently lower-ized caseically speaking
            for attr in attrs:
                if attr[0] == 'id':
                    if attr[1] == gstrClassname: # name that class
                        gfImporting = True

    def handle_endtag(self, tag):
        global gfImporting
        if tag == 'div': ## Div fails. case sensitive html!
            gfImporting = False
            gInsertSought = False

    def handle_data(self, data):
        global gfImporting
        if gfImporting==True:
            mainoutfile.write(S3BinaryHostReplace(data)) # show contents when we're importing

fname = sys.argv[1]                # arg1 is source pathspec
fname = fname.replace("ref/", "")  # remove /ref source prefix, if found
fname = fname.replace(".md", "")   # remove .md suffix, so I can add S1, S2 to core fname before .md
mainoutfile = open("tmp/" + fname + ".md", "w")
slide = 1

parser = MyHTMLParser()
for line in sys.stdin:
    # if we want a slice, start with # see, and get a hyperlink of See Foobat, and new file desty Foobat
    if line.lower().startswith('# see '):
        slide = slide + 1
        mainoutfile.write('**Next: [' + line[5:].strip() + '](' + fname + 'S' + str(slide) + '.html)**  \r\n\r\n')
        mainoutfile.close()
        mainoutfile = open("tmp/" + fname + "S" + str(slide) + ".md", "w")
        line = '# ' + line[6:]  # The top title is # See Foobat minus the # See portion

    if line.lower().startswith('insert:'):
        gInsertSought = True
        params = line[len('insert:'):].split()
#        filename ='src/' +  params[0]
        gstrClassname = params[1]                   # set this global
        insertfilename = 'src/' + params[0]
        if False == os.path.isfile(insertfilename):
            insertfilename = 'src/ref/' + params[0] # sometimes i forget to prepend the ref/
        if False == os.path.isfile(insertfilename):
            insertfilename = 'src/ref/_' + params[0] #refs have the underscore but i can omit it (should be sure there's no same name in root!)
        if False == os.path.isfile(insertfilename):
            print("insert: source file not found, so ctrl-c and fix: " + insertfilename)
            time.sleep(99999)
            sys.exit() # won't stop this but worthy effort
        f =  open(insertfilename, "r")
	parser.feed(f.read())
        parser.close()
    else:
        mainoutfile.write(S3BinaryHostReplace(line)),

if True == gInsertSought:
    print("insert id not found, so ctrl-c and fix. but this is broken") 
#    time.sleep(99999)

mainoutfile.close()
