# remote-ac-homeserver

The goal of this project is to create a device that can automate my
IR-controllable AC units, either using a DHT sensor or over the internet
while I'm away.

This is the "central server" part of the [remote-ac-controller] project.

[remote-ac-controller]:https://github.com/prplecake/remote-ac-controller

<video controls src="https://user-images.githubusercontent.com/83595468/212430312-2f6c45ab-76fd-4837-91fb-a6bcf532ea33.mp4"></video>

## quickstart

requirements:

- [lirc](https://www.lirc.org/)
- nodejs >= v16
- redis

```shell
# setup virtualenv and install packages
python -m virtualenv venv
source ./venv/bin/activate
pip install -r requirements.txt
# install node packages
npm install
# configure app
cp ac_ctl_web/env.example.py ac_ctl_web/env.py
$EDITOR ac_ctl_web/env.py
# database migrations
python manage.py migrate
# start development server
honcho start
```

## deploying

Basically the same but instead of using the development server you'll want 
to compile the webpack:

```shell
# after npm install
npm run build
```

For "deployment" it would be better to use a reverse proxy like nginx.
See [`dist/`][dist].

[dist]: https://git.fminus.co/prplecake/remote-ac-homeserver/src/branch/master/dist

This gets the web interface set up along with the DHT sensor. Learn how to set
up lirc [on the wiki][lirc-wiki].

[lirc-wiki]: https://gitfminus.co/prplecake/remote-ac-homeserver/wiki/lirc

## see also

- [circuit schematic](https://github.com/prplecake/remote-ac-homeserver/wiki/Schematic)
- [frigidaire ac remote lirc config](https://gist.github.com/prplecake/71c4bc8584541cf7423b922b81733c3a)
- [IRreceiver MCU code](https://git.fminus.co/prplecake/IRreceiver)
