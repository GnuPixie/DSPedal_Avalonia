import os
import sys
import chardet

# create a text file called "current_files"
i = 0
while True:
    filename = f"current_files{i}.txt"
    if not os.path.isfile(filename):
        break
    i += 1

with open(filename, "w", encoding="utf-8") as f:

    # loop through the current working directory
    for filename in os.listdir(os.getcwd()):

        # check if the file ends with .xaml or .cs
        if filename.endswith(".xaml") or filename.endswith(".cs") or filename.endswith(".axaml"):

            # write the file name to the text file
            f.write(filename + "\n")

            # try to open the file and read its contents
            try:
                # open the file in binary mode
                with open(filename, "rb") as g:
                    # read the raw bytes
                    rawdata = g.read()
                    # detect the encoding
                    result = chardet.detect(rawdata)
                    # get the encoding name
                    encoding = result["encoding"]

                # open the file again with the detected encoding
                with open(filename, "r", encoding=encoding) as g:
                    contents = g.read()

                # write the contents to the text file in a code block
                f.write("```\n")
                f.write(contents + "\n")
                f.write("```\n")

            # handle the exception if the file cannot be opened or decoded
            except (IOError, UnicodeDecodeError):
                f.write("The file could not be opened or decoded.\n")

            # write a blank line to separate the files
            f.write("\n")

# wait for the user to press enter before exiting
input("Press enter to exit.")
sys.exit()
