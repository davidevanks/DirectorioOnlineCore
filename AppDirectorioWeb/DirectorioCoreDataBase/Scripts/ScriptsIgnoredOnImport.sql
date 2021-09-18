--
-- PostgreSQL database dump
--

-- Dumped from database version 13.4
-- Dumped by pg_dump version 13.4

-- Started on 2021-09-18 17:35:44

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 4 (class 2615 OID 16804)
-- Name: dbo; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA dbo;


ALTER SCHEMA dbo OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 215 (class 1259 OID 16824)
-- Name: IdentityLogin; Type: TABLE; Schema: dbo; Owner: postgres
--

CREATE TABLE dbo."IdentityLogin" (
    "LoginProvider" character varying(256) NOT NULL,
    "ProviderKey" character varying(128) NOT NULL,
    "UserId" integer NOT NULL,
    "Name" character varying(256) NOT NULL
);


ALTER TABLE dbo."IdentityLogin" OWNER TO postgres;

--
-- TOC entry 214 (class 1259 OID 16818)
-- Name: IdentityRole; Type: TABLE; Schema: dbo; Owner: postgres
--

CREATE TABLE dbo."IdentityRole" (
    "Id" integer NOT NULL,
    "Name" character varying(50) NOT NULL
);


ALTER TABLE dbo."IdentityRole" OWNER TO postgres;

--
-- TOC entry 220 (class 1259 OID 16870)
-- Name: IdentityRoleClaim; Type: TABLE; Schema: dbo; Owner: postgres
--

CREATE TABLE dbo."IdentityRoleClaim" (
    "Id" integer NOT NULL,
    "RoleId" integer NOT NULL,
    "ClaimType" character varying(256) NOT NULL,
    "ClaimValue" character varying(256)
);


ALTER TABLE dbo."IdentityRoleClaim" OWNER TO postgres;

--
-- TOC entry 219 (class 1259 OID 16868)
-- Name: IdentityRoleClaim_Id_seq; Type: SEQUENCE; Schema: dbo; Owner: postgres
--

CREATE SEQUENCE dbo."IdentityRoleClaim_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE dbo."IdentityRoleClaim_Id_seq" OWNER TO postgres;

--
-- TOC entry 3088 (class 0 OID 0)
-- Dependencies: 219
-- Name: IdentityRoleClaim_Id_seq; Type: SEQUENCE OWNED BY; Schema: dbo; Owner: postgres
--

ALTER SEQUENCE dbo."IdentityRoleClaim_Id_seq" OWNED BY dbo."IdentityRoleClaim"."Id";


--
-- TOC entry 213 (class 1259 OID 16816)
-- Name: IdentityRole_Id_seq; Type: SEQUENCE; Schema: dbo; Owner: postgres
--

CREATE SEQUENCE dbo."IdentityRole_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE dbo."IdentityRole_Id_seq" OWNER TO postgres;

--
-- TOC entry 3089 (class 0 OID 0)
-- Dependencies: 213
-- Name: IdentityRole_Id_seq; Type: SEQUENCE OWNED BY; Schema: dbo; Owner: postgres
--

ALTER SEQUENCE dbo."IdentityRole_Id_seq" OWNED BY dbo."IdentityRole"."Id";


--
-- TOC entry 212 (class 1259 OID 16807)
-- Name: IdentityUser; Type: TABLE; Schema: dbo; Owner: postgres
--

CREATE TABLE dbo."IdentityUser" (
    "UserName" character varying(256) NOT NULL,
    "Email" character varying(256) NOT NULL,
    "EmailConfirmed" boolean NOT NULL,
    "IdPlan" integer,
    "PasswordHash" text,
    "SecurityStamp" character varying(38),
    "PhoneNumber" character varying(50),
    "PhoneNumberConfirmed" boolean,
    "TwoFactorEnabled" boolean NOT NULL,
    "LockoutEnd" timestamp without time zone,
    "LockoutEnabled" boolean NOT NULL,
    "AccessFailedCount" integer NOT NULL,
    "Id" integer NOT NULL
);


ALTER TABLE dbo."IdentityUser" OWNER TO postgres;

--
-- TOC entry 217 (class 1259 OID 16839)
-- Name: IdentityUserClaim; Type: TABLE; Schema: dbo; Owner: postgres
--

CREATE TABLE dbo."IdentityUserClaim" (
    "Id" integer NOT NULL,
    "UserId" integer NOT NULL,
    "ClaimType" character varying(256) NOT NULL,
    "ClaimValue" character varying(256) NOT NULL
);


ALTER TABLE dbo."IdentityUserClaim" OWNER TO postgres;

--
-- TOC entry 216 (class 1259 OID 16837)
-- Name: IdentityUserClaim_Id_seq; Type: SEQUENCE; Schema: dbo; Owner: postgres
--

CREATE SEQUENCE dbo."IdentityUserClaim_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE dbo."IdentityUserClaim_Id_seq" OWNER TO postgres;

--
-- TOC entry 3090 (class 0 OID 0)
-- Dependencies: 216
-- Name: IdentityUserClaim_Id_seq; Type: SEQUENCE OWNED BY; Schema: dbo; Owner: postgres
--

ALTER SEQUENCE dbo."IdentityUserClaim_Id_seq" OWNED BY dbo."IdentityUserClaim"."Id";


--
-- TOC entry 218 (class 1259 OID 16853)
-- Name: IdentityUserRole; Type: TABLE; Schema: dbo; Owner: postgres
--

CREATE TABLE dbo."IdentityUserRole" (
    "UserId" integer NOT NULL,
    "RoleId" integer NOT NULL
);


ALTER TABLE dbo."IdentityUserRole" OWNER TO postgres;

--
-- TOC entry 211 (class 1259 OID 16805)
-- Name: IdentityUser_Id_seq; Type: SEQUENCE; Schema: dbo; Owner: postgres
--

CREATE SEQUENCE dbo."IdentityUser_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE dbo."IdentityUser_Id_seq" OWNER TO postgres;

--
-- TOC entry 3091 (class 0 OID 0)
-- Dependencies: 211
-- Name: IdentityUser_Id_seq; Type: SEQUENCE OWNED BY; Schema: dbo; Owner: postgres
--

ALTER SEQUENCE dbo."IdentityUser_Id_seq" OWNED BY dbo."IdentityUser"."Id";


--
-- TOC entry 202 (class 1259 OID 16732)
-- Name: AnuncioInfo; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AnuncioInfo" (
    "NombreNegocio" character varying(500) NOT NULL,
    "IdCategoria" integer NOT NULL,
    "DescripcionNegocio" text,
    "IdPais" integer NOT NULL,
    "IdDepartamento" integer NOT NULL,
    "DireccionNegocio" text NOT NULL,
    "Longitud" integer,
    "Latitud" integer,
    "TelefonoNegocio" character varying(8) NOT NULL,
    "EmailNegocio" character varying(100),
    "PaginaWebNegocio" character varying(200),
    "InstagramNegocio" character varying(200),
    "FacebookNegocio" character varying(200),
    "TwitterNegocio" character varying(200),
    "WhatsApp" character varying(8),
    "TieneDelivery" bit(1),
    "Hugo" bit(1),
    "PedidosYa" bit(1),
    "Piki" bit(1),
    "FechaCreacion" date,
    "FechaModificacion" date,
    "IdUsuarioCreacion" bigint,
    "IdUsuarioModificacion" bigint,
    "Id" bigint NOT NULL,
    "HabilitarHorarioFeriado" bit(1),
    "Activo" bit(1),
    "Calificacion" integer,
    "LogoNegocio" character varying,
    "Idusuario" bigint
);


ALTER TABLE public."AnuncioInfo" OWNER TO postgres;

--
-- TOC entry 201 (class 1259 OID 16730)
-- Name: AnuncioInfo_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."AnuncioInfo" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."AnuncioInfo_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 204 (class 1259 OID 16742)
-- Name: CatCatalogos; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."CatCatalogos" (
    "Id" integer NOT NULL,
    "IdPadre" integer NOT NULL,
    "Nombre" character varying(500) NOT NULL,
    "Activo" bit(1),
    "FechaCreacion" date,
    "FechaModificacion" date,
    "IdUsuarioCreacion" bigint,
    "IdUsuarioModificacion" bigint
);


ALTER TABLE public."CatCatalogos" OWNER TO postgres;

--
-- TOC entry 203 (class 1259 OID 16740)
-- Name: CatCatalogos_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."CatCatalogos" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."CatCatalogos_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 206 (class 1259 OID 16752)
-- Name: FotosAnuncio; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."FotosAnuncio" (
    "Id" bigint NOT NULL,
    "IdNegocio" bigint,
    "FechaCreacion" date,
    "FechaModificacion" date,
    "IdUsuarioModificacion" bigint,
    "IdUsuarioCreacion" bigint,
    "Foto" character varying(200)
);


ALTER TABLE public."FotosAnuncio" OWNER TO postgres;

--
-- TOC entry 205 (class 1259 OID 16750)
-- Name: FotosAnuncio_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."FotosAnuncio" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."FotosAnuncio_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 208 (class 1259 OID 16759)
-- Name: HorarioAtencionNegocio; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."HorarioAtencionNegocio" (
    "Id" bigint NOT NULL,
    "Dia" character varying(100),
    "HoraInicio" character varying(100),
    "HoraFin" character varying(100),
    "FechaCreacion" date,
    "FechaModificacion" date,
    "IdUsuarioCreacion" bigint,
    "IdUsuarioModificacion" bigint,
    "IdNegocio" bigint
);


ALTER TABLE public."HorarioAtencionNegocio" OWNER TO postgres;

--
-- TOC entry 207 (class 1259 OID 16757)
-- Name: HorarioAtencionNegocio_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."HorarioAtencionNegocio" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."HorarioAtencionNegocio_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 210 (class 1259 OID 16766)
-- Name: ReviewsNegocio; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."ReviewsNegocio" (
    "Id" integer NOT NULL,
    "IdNegocio" bigint,
    "ReviewDescripcion" text,
    "Calificacion" integer,
    "IdUsuario" bigint,
    "IdIsuarioCreacion" bigint,
    "IdUsuarioEdicion" bigint,
    "FechaCreacion" date,
    "FechaModificacion" date,
    "Activo" bit(1)
);


ALTER TABLE public."ReviewsNegocio" OWNER TO postgres;

--
-- TOC entry 209 (class 1259 OID 16764)
-- Name: ReviewsNegocio_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."ReviewsNegocio" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."ReviewsNegocio_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 2915 (class 2604 OID 16821)
-- Name: IdentityRole Id; Type: DEFAULT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY dbo."IdentityRole" ALTER COLUMN "Id" SET DEFAULT nextval('dbo."IdentityRole_Id_seq"'::regclass);


--
-- TOC entry 2917 (class 2604 OID 16873)
-- Name: IdentityRoleClaim Id; Type: DEFAULT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY dbo."IdentityRoleClaim" ALTER COLUMN "Id" SET DEFAULT nextval('dbo."IdentityRoleClaim_Id_seq"'::regclass);


--
-- TOC entry 2914 (class 2604 OID 16810)
-- Name: IdentityUser Id; Type: DEFAULT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY dbo."IdentityUser" ALTER COLUMN "Id" SET DEFAULT nextval('dbo."IdentityUser_Id_seq"'::regclass);


--
-- TOC entry 2916 (class 2604 OID 16842)
-- Name: IdentityUserClaim Id; Type: DEFAULT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY dbo."IdentityUserClaim" ALTER COLUMN "Id" SET DEFAULT nextval('dbo."IdentityUserClaim_Id_seq"'::regclass);


--
-- TOC entry 2934 (class 2606 OID 16831)
-- Name: IdentityLogin IdentityLogin_pkey; Type: CONSTRAINT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY dbo."IdentityLogin"
    ADD CONSTRAINT "IdentityLogin_pkey" PRIMARY KEY ("LoginProvider", "ProviderKey", "UserId");


--
-- TOC entry 2940 (class 2606 OID 16878)
-- Name: IdentityRoleClaim IdentityRoleClaim_pkey; Type: CONSTRAINT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY dbo."IdentityRoleClaim"
    ADD CONSTRAINT "IdentityRoleClaim_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 2932 (class 2606 OID 16823)
-- Name: IdentityRole IdentityRole_pkey; Type: CONSTRAINT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY dbo."IdentityRole"
    ADD CONSTRAINT "IdentityRole_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 2936 (class 2606 OID 16847)
-- Name: IdentityUserClaim IdentityUserClaim_pkey; Type: CONSTRAINT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY dbo."IdentityUserClaim"
    ADD CONSTRAINT "IdentityUserClaim_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 2938 (class 2606 OID 16857)
-- Name: IdentityUserRole IdentityUserRole_pkey; Type: CONSTRAINT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY dbo."IdentityUserRole"
    ADD CONSTRAINT "IdentityUserRole_pkey" PRIMARY KEY ("UserId", "RoleId");


--
-- TOC entry 2930 (class 2606 OID 16815)
-- Name: IdentityUser PK_IdentityUser; Type: CONSTRAINT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY dbo."IdentityUser"
    ADD CONSTRAINT "PK_IdentityUser" PRIMARY KEY ("Id");


--
-- TOC entry 2919 (class 2606 OID 16739)
-- Name: AnuncioInfo AnuncioInfo_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AnuncioInfo"
    ADD CONSTRAINT "AnuncioInfo_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 2922 (class 2606 OID 16749)
-- Name: CatCatalogos CatCatalogos_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."CatCatalogos"
    ADD CONSTRAINT "CatCatalogos_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 2924 (class 2606 OID 16756)
-- Name: FotosAnuncio FotosAnuncio_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."FotosAnuncio"
    ADD CONSTRAINT "FotosAnuncio_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 2926 (class 2606 OID 16763)
-- Name: HorarioAtencionNegocio HorarioAtencionNegocio_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."HorarioAtencionNegocio"
    ADD CONSTRAINT "HorarioAtencionNegocio_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 2928 (class 2606 OID 16773)
-- Name: ReviewsNegocio ReviewsNegocio_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ReviewsNegocio"
    ADD CONSTRAINT "ReviewsNegocio_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 2920 (class 1259 OID 16896)
-- Name: fki_AnuncioInfo_IdUser_fkey; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "fki_AnuncioInfo_IdUser_fkey" ON public."AnuncioInfo" USING btree ("Idusuario");


--
-- TOC entry 2948 (class 2606 OID 16832)
-- Name: IdentityLogin IdentityLogin_UserId_fkey; Type: FK CONSTRAINT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY dbo."IdentityLogin"
    ADD CONSTRAINT "IdentityLogin_UserId_fkey" FOREIGN KEY ("UserId") REFERENCES dbo."IdentityUser"("Id");


--
-- TOC entry 2952 (class 2606 OID 16879)
-- Name: IdentityRoleClaim IdentityRoleClaim_RoleId_fkey; Type: FK CONSTRAINT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY dbo."IdentityRoleClaim"
    ADD CONSTRAINT "IdentityRoleClaim_RoleId_fkey" FOREIGN KEY ("RoleId") REFERENCES dbo."IdentityRole"("Id");


--
-- TOC entry 2949 (class 2606 OID 16848)
-- Name: IdentityUserClaim IdentityUserClaim_UserId_fkey; Type: FK CONSTRAINT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY dbo."IdentityUserClaim"
    ADD CONSTRAINT "IdentityUserClaim_UserId_fkey" FOREIGN KEY ("UserId") REFERENCES dbo."IdentityUser"("Id");


--
-- TOC entry 2950 (class 2606 OID 16858)
-- Name: IdentityUserRole IdentityUserRole_RoleId_fkey; Type: FK CONSTRAINT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY dbo."IdentityUserRole"
    ADD CONSTRAINT "IdentityUserRole_RoleId_fkey" FOREIGN KEY ("RoleId") REFERENCES dbo."IdentityRole"("Id");


--
-- TOC entry 2951 (class 2606 OID 16863)
-- Name: IdentityUserRole IdentityUserRole_UserId_fkey; Type: FK CONSTRAINT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY dbo."IdentityUserRole"
    ADD CONSTRAINT "IdentityUserRole_UserId_fkey" FOREIGN KEY ("UserId") REFERENCES dbo."IdentityUser"("Id");


--
-- TOC entry 2941 (class 2606 OID 16774)
-- Name: AnuncioInfo AnuncioInfo_IdCategoria_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AnuncioInfo"
    ADD CONSTRAINT "AnuncioInfo_IdCategoria_fkey" FOREIGN KEY ("IdCategoria") REFERENCES public."CatCatalogos"("Id") NOT VALID;


--
-- TOC entry 2943 (class 2606 OID 16784)
-- Name: AnuncioInfo AnuncioInfo_IdDepartamento_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AnuncioInfo"
    ADD CONSTRAINT "AnuncioInfo_IdDepartamento_fkey" FOREIGN KEY ("IdDepartamento") REFERENCES public."CatCatalogos"("Id") NOT VALID;


--
-- TOC entry 2942 (class 2606 OID 16779)
-- Name: AnuncioInfo AnuncioInfo_IdPais_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AnuncioInfo"
    ADD CONSTRAINT "AnuncioInfo_IdPais_fkey" FOREIGN KEY ("IdPais") REFERENCES public."CatCatalogos"("Id") NOT VALID;


--
-- TOC entry 2944 (class 2606 OID 16891)
-- Name: AnuncioInfo AnuncioInfo_IdUser_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AnuncioInfo"
    ADD CONSTRAINT "AnuncioInfo_IdUser_fkey" FOREIGN KEY ("Idusuario") REFERENCES dbo."IdentityUser"("Id") NOT VALID;


--
-- TOC entry 2945 (class 2606 OID 16789)
-- Name: FotosAnuncio FotosAnuncio_IdNegocio_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."FotosAnuncio"
    ADD CONSTRAINT "FotosAnuncio_IdNegocio_fkey" FOREIGN KEY ("IdNegocio") REFERENCES public."AnuncioInfo"("Id") NOT VALID;


--
-- TOC entry 2946 (class 2606 OID 16794)
-- Name: HorarioAtencionNegocio HorarioAtencionNegocio_IdNegocio_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."HorarioAtencionNegocio"
    ADD CONSTRAINT "HorarioAtencionNegocio_IdNegocio_fkey" FOREIGN KEY ("IdNegocio") REFERENCES public."AnuncioInfo"("Id") NOT VALID;


--
-- TOC entry 2947 (class 2606 OID 16799)
-- Name: ReviewsNegocio ReviewsNegocio_IdNegocio_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ReviewsNegocio"
    ADD CONSTRAINT "ReviewsNegocio_IdNegocio_fkey" FOREIGN KEY ("IdNegocio") REFERENCES public."AnuncioInfo"("Id") NOT VALID;


-- Completed on 2021-09-18 17:35:44

--
-- PostgreSQL database dump complete
--

