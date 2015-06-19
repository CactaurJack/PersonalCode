import httplib2
import pprint
import time
import praw
import codecs

from apiclient.discovery import build
from apiclient.http import MediaFileUpload
from oauth2client.client import OAuth2WebServerFlow
from apiclient import errors

#user: movielinkbot
#pass: *****
#user_agent = ("Movie Link Bot 1.0 by /u/CactuarJack")

#Google login stuff
CLIENT_ID = '298789702393-39lkm89qi4rh5utjev53ocqkibljh5tb.apps.googleusercontent.com'
CLIENT_SECRET = ********
OAUTH_SCOPE = 'https://www.googleapis.com/auth/drive'
REDIRECT_URI = 'urn:ietf:wg:oauth:2.0:oob'
FILENAME = 'movielist.txt'

#Google API login  ONLY DO ONCE TAKEs FOREVER
flow = OAuth2WebServerFlow(CLIENT_ID, CLIENT_SECRET, OAUTH_SCOPE, REDIRECT_URI)
authorize_url = flow.step1_get_authorize_url()
print 'Go to the following link in your browser: ' + authorize_url
code = raw_input('Enter verification code: ').strip()
credentials = flow.step2_exchange(code)

#Reddit Login

 #user: movielinkbot
 #pass: ******
 #user_agent = ("Movie Link Bot 1.0 by /u/CactuarJack")

r = praw.Reddit("Movie Link Bot 1.0 by /u/CactuarJack")
r.login("movielinkbot", "******")
subreddit = r.get_subreddit('fullmoviesonyoutube')
last = 'last'
current = 'current'
check = 0

# Create an httplib2.Http object and authorize it with our credentials
##http = httplib2.Http()
##http = credentials.authorize(http)
##
##drive_service = build('drive', 'v2', http=http)

#Creates a new file each time

file_id = '0Byf-TWn0JtjLeTh6ZnNsWHE1QUU'

def update_file(service, file_id, new_title, new_description, new_mime_type,
                new_filename, new_revision):

    try:
         # First retrieve the file from the API.
        file = service.files().get(fileId=file_id).execute()

        # File's new metadata.
        file['title'] = new_title
        file['description'] = new_description
        file['mimeType'] = new_mime_type

        # File's new content.
        media_body = MediaFileUpload(
            'movielist.txt', mimetype=new_mime_type, resumable=True)

        # Send the request to the API.
        
        updated_file = service.files().update(
            fileId=file_id,
            body=file,
            newRevision=new_revision,
            media_body=media_body).execute()
        return updated_file
    except errors.HttpError, error:
        print 'An error occurred: %s' % error
        return None

#Main Loop
#Needs to read from reddit, add to movielist.txt, update google doc from movielist.txt and then sleep for 60 seconds

while 1 == 1:

    http = httplib2.Http()
    http = credentials.authorize(http)

    drive_service = build('drive', 'v2', http=http)
 
    dataFile = codecs.open('movielist.txt', 'a', 'utf-8')
    
    for submission in subreddit.get_new(limit=10):
        
        if submission.is_self :
           continue

        if current == submission.id :
            break
        
        else:
            if check == 0:
                last = submission.id
                check = 1
                
            dataFile.write(submission.title + '\r\n' + submission.url + '\r\n \r\n')
            print submission.title + '\r\n' + submission.url + '\r\n \r\n'
            
    current = last
    dataFile.close()
    check = 0
    print "File closed"

    update_file(drive_service, file_id, 'Movie List', 'movielist', 'text/plain', FILENAME, 'false')

    #Google upload stuff  
    time.sleep(60)



