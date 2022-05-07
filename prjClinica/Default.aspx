<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="prjClinica._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script src="Scripts/MascaraDigitacao.js"></script>

    <p></p>
    <hr class="linhaEscura" />
      <h1> <asp:Label ID="lbTitulo" runat="server"  /></h1>
    <hr class="linhaEscura" />

    <p></p>
    <p></p>
    <p></p>

    <ul style="list-style-type: none">

      
        <li>
          <hr class="linhaEscura" />
        </li>

        <li>
            <asp:Label ID="lbNome" runat="server" Width="130px" /><asp:TextBox ID="txNome" runat="server" Width="300px"></asp:TextBox>
        </li>
        <li>
            <asp:Label ID="lbSexo" runat="server" Width="130px" /> 
            <asp:Label ID="lbMasc" runat="server"/>&nbsp;<asp:RadioButton ID="rbMasc" runat="server" GroupName="sexo" />&nbsp;
            <asp:Label ID="lbFem" runat="server" />&nbsp;<asp:RadioButton ID="rbFem"  runat="server" GroupName="sexo" />&nbsp;
        </li>
        <li>
            <asp:Label ID="lbDataNascimento" runat="server" Width="130px" /><asp:TextBox ID="txDataNascimento" runat="server" Width="100px"></asp:TextBox>
        </li>
        <li>
             <asp:Label ID="lbPeso" runat="server" Width="130px" /><asp:TextBox ID="txPeso" runat="server" Width="100px" ></asp:TextBox>
        </li>
        <li>
             <asp:Label ID="lbAltura" runat="server" Width="130px" /><asp:TextBox ID="txAltura" runat="server" Width="100px" ></asp:TextBox>
        </li>
         <li>
          <hr class="linhaEscura" />
        </li>
        <li>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lbCadastrar" runat="server" />
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>
                        <asp:Label ID="lbBuscarPeloNome" runat="server" />
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>
                        <asp:Label ID="lbBuscarPeloId" runat="server" />
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btOk" runat="server" Text="OK" Width="40px" OnClick="btOk_Click" /></td>
                    <td style="width: 40px">&nbsp;</td>
                    <td>

                        <asp:TextBox ID="buscarPeloNome" runat="server" Width="100px"></asp:TextBox>
                        <asp:Button ID="btBuscarPeloNome" runat="server" Text="OK" Width="40px" OnClick="btBuscarPeloNome_Click" />
                    </td>
                    <td style="width: 40px">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="buscarPeloId" runat="server" Width="50px"></asp:TextBox>
                        <asp:Button ID="btBuscarPeloId" runat="server" Text="OK" Width="40px" OnClick="btBuscarPeloId_Click" />
                    </td>
                    <td style="width: 100px">&nbsp; 
                    </td>
                    <td style="width: 100px">
                        <asp:Button ID="btEdita" runat="server" Text="Edita" Width="70px" Visible="false" OnClick="btEdita_Click" />
                    </td>
                    <td style="width: 100px">
                        <asp:Button ID="btExclui" runat="server" Text="Exclui" Width="70px" Visible="false" OnClick="btExclui_Click" />
                    </td>
                </tr>

                 </table>
            </li>

           <li>

                <asp:Label ID="Label1" runat="server" Width="130px" />
                <asp:Label ID="Label2" Text="ID" runat="server" />&nbsp;
                   <asp:RadioButton ID="id" runat="server" GroupName="ordem" AutoPostBack="true" OnCheckedChanged="id_CheckedChanged" />&nbsp;
            <asp:Label ID="Label3" runat="server" />&nbsp;
                   <asp:RadioButton ID="nome" Text="NOME" runat="server" GroupName="ordem" AutoPostBack="true" OnCheckedChanged="id_CheckedChanged" />&nbsp;
         
           
</li>
        
         <li>
           <hr class="linhaEscura" />
             
        </li>
        <li>
                   <asp:Label ID="mensagem" runat="server"  /> 
        </li>
        <li>
            <asp:Literal ID="txRelatorio" runat="server"></asp:Literal>
        </li>

    </ul>

</asp:Content>