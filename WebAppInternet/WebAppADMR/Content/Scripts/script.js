

function afficher() {
	
	document.getElementById('ssmenu').style.display = 'table';
	
}

function cacher() {
	
	document.getElementById('ssmenu').style.display = 'none';

}



function controle() {
	
	ndcl = document.getElementById('id_ndc').value.length;
	mdpl = document.getElementById('id_mdp').value.length;
	ndc = document.getElementById('id_ndc').value;
	mdp = document.getElementById('id_mdp').value;
	
	if (ndcl > 9 && mdpl > 5) 
	{
		
		alert("Le nom de compte : " + ndc + ", et le mot de passe : " + mdp  + ", ont été envoyés.");
		
	}
	
	
	else if (ndcl < 10)
	{
		
		
		alert("Le nom de compte doit comporter au moins 10 caractères.");
		document.getElementById('id_ndc').focus();
		
	}
	
	else if (ndcl > 9 && mdpl < 6)
	{
		
		alert("Le mot de passe doit comporter au moins 6 caractères.");
		document.getElementById('id_mdp').focus();
		
	}
	
	else
	{
		alert("erreur");
		
	}
	
	
}



