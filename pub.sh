#!/bin/bash
set -x

mkdir tmp/src
mkdir tmp/ref

# clear out the target where we'll assemble all the files
cd tmp
rm *
cd ..

# grab all the .md files somewhere off src
cd src
find . | grep '\.md' | sed 's/\.\///g' > ../sources.txt
cd ..

while IFS= read -r file
do
cat src/$file | python insertions-and-slideshows.py $file
done < "sources.txt"

rm sources.txt

cd tmp
# mv ref/* .  removed cuz the python app saves to tmp/ PERIOD
ls -1 *.md | cut -f 1 -d '.' > sources.txt
while IFS= read -r file
do
echo '<meta name="viewport" content="width=device-width, initial-scale=1.0"><link rel="stylesheet" type="text/css" media="all" href="ds-style.css" /><div id="main">' > $file.html
md2html $file.md -s solarized-dark.css >> $file.html
# https://www.npmjs.com/package/markdown-to-html
# this is installed with -g, so md2html works, but the next-page insertions might$
echo '</div>' >> $file.html

# because md2html escapes critical syntax, i unescape it.
sed -i -e 's/&lt;/</g' $file.html
sed -i -e 's/&gt;/>/g' $file.html
sed -i -e "s/&#39;/'/g" $file.html

sudo cp $file.html /var/www/html/pj/
# cp $file.html ../html
done < "sources.txt"

# there are two different sources.txt files,
# one's in tmp/sources.txt, and they differ in content!

rm sources.txt

cd ../src/root-files
sudo cp -r * /var/www/html/pj/
cd ..
