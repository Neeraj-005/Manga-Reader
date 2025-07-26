from flask import Flask, jsonify, send_from_directory
from flask_cors import CORS
import os

app = Flask(__name__)
CORS(app)

arr = ['op', 'naruto', 'bluelock', 'aot','black clover']

file = {
    'op': ['op1.pdf', 'op2.pdf'],
    'naruto': ['naruto1.pdf'],
    'bluelock': ['bl1.pdf', 'bl2.pdf'],
    'aot': ['aot1.pdf', 'aot2.pdf', 'aot3.pdf']
}

@app.route('/')
def get_anime():
    return jsonify(arr)

@app.route('/files/<anime_name>')
def get_files(anime_name):
    if anime_name not in file:
        return jsonify({"error": "Anime not found"}), 404
    return jsonify(file[anime_name])

@app.route('/files/<anime_name>/<filename>')
def serve_pdf(anime_name, filename):
    folder_path = os.path.join(os.getcwd(), 'files', anime_name)
    return send_from_directory(folder_path, filename)

if __name__ == '__main__':
    app.run(debug=True)
