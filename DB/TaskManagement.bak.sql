PGDMP                      }           TaskManagement    16.4    16.4     �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            �           1262    25068    TaskManagement    DATABASE     �   CREATE DATABASE "TaskManagement" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'English_United States.1252';
     DROP DATABASE "TaskManagement";
                postgres    false                        2615    2200    public    SCHEMA        CREATE SCHEMA public;
    DROP SCHEMA public;
                pg_database_owner    false            �           0    0    SCHEMA public    COMMENT     6   COMMENT ON SCHEMA public IS 'standard public schema';
                   pg_database_owner    false    4            �            1259    25069    Projects    TABLE     �   CREATE TABLE public."Projects" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "CreatedDate" timestamp without time zone NOT NULL
);
    DROP TABLE public."Projects";
       public         heap    postgres    false    4            �            1259    25076 	   TaskItems    TABLE     �   CREATE TABLE public."TaskItems" (
    "Id" uuid NOT NULL,
    "Title" text NOT NULL,
    "Description" text NOT NULL,
    "DueDate" timestamp without time zone NOT NULL,
    "Status" integer NOT NULL,
    "ProjectId" uuid NOT NULL
);
    DROP TABLE public."TaskItems";
       public         heap    postgres    false    4            �          0    25069    Projects 
   TABLE DATA           A   COPY public."Projects" ("Id", "Name", "CreatedDate") FROM stdin;
    public          postgres    false    215   �       �          0    25076 	   TaskItems 
   TABLE DATA           e   COPY public."TaskItems" ("Id", "Title", "Description", "DueDate", "Status", "ProjectId") FROM stdin;
    public          postgres    false    216                     2606    25075    Projects Projects_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public."Projects"
    ADD CONSTRAINT "Projects_pkey" PRIMARY KEY ("Id");
 D   ALTER TABLE ONLY public."Projects" DROP CONSTRAINT "Projects_pkey";
       public            postgres    false    215                        2606    25082    TaskItems TaskItems_pkey 
   CONSTRAINT     \   ALTER TABLE ONLY public."TaskItems"
    ADD CONSTRAINT "TaskItems_pkey" PRIMARY KEY ("Id");
 F   ALTER TABLE ONLY public."TaskItems" DROP CONSTRAINT "TaskItems_pkey";
       public            postgres    false    216            !           2606    25083 )   TaskItems FK_TaskItems_Projects_ProjectId    FK CONSTRAINT     �   ALTER TABLE ONLY public."TaskItems"
    ADD CONSTRAINT "FK_TaskItems_Projects_ProjectId" FOREIGN KEY ("ProjectId") REFERENCES public."Projects"("Id") ON DELETE CASCADE;
 W   ALTER TABLE ONLY public."TaskItems" DROP CONSTRAINT "FK_TaskItems_Projects_ProjectId";
       public          postgres    false    215    4638    216            �   v   x�U̱!���"a���)2@���H)���U��zՄ�QQQ*�I�2O�<����Z�+��%%ƍkt������WoJT�2��T�t�����<��zN�	1��#e$j      �   �   x�}�1n1D�Z:E.@��R�ֵ��2�$���6��Y�r�~>&E��-�f0���{CR��9��\.�\?����~������3�3�PIs�G
�KAV�]���ڬ�r*O��@�3�}k��#��j/��Y����4�8rLe�tU0G�$��f�:�Z� \^=�     