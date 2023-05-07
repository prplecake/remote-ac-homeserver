# Generated by Django 4.2 on 2023-05-07 16:12

from django.db import migrations, models


class Migration(migrations.Migration):
    replaces = [('ac_ctl', '0001_initial'), ('ac_ctl', '0002_dhtsensordata')]

    initial = True

    dependencies = [
    ]

    operations = [
        migrations.CreateModel(
            name='State',
            fields=[
                ('id', models.BigAutoField(auto_created=True, primary_key=True,
                                           serialize=False, verbose_name='ID')),
                ('ac_unit_on', models.BooleanField()),
            ],
            options={
                'db_table': 'app_state',
            },
        ),
        migrations.CreateModel(
            name='DhtSensorData',
            fields=[
                ('id', models.BigAutoField(auto_created=True, primary_key=True,
                                           serialize=False, verbose_name='ID')),
                ('date', models.DateTimeField()),
                ('temp_c', models.FloatField()),
                ('humidity', models.FloatField()),
            ],
            options={
                'verbose_name': 'DHT Sensor Data',
                'verbose_name_plural': 'DHT Sensor Data',
            },
        ),
    ]
