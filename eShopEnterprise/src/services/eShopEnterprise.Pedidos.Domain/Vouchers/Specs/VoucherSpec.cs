using eShopEnterprise.Core.Specification;
using System;
using System.Linq.Expressions;

namespace eShopEnterprise.Pedidos.Domain.Vouchers.Specs
{
    public class VoucherDataSpecification : Specification<Voucher>
    {
        public override Expression<Func<Voucher, bool>> ToExpression()
        {
            return voucher => voucher.DataValidade >= DateTime.Now;
        }
    }

    public class VoucherQuantidadeSpecification : Specification<Voucher>
    {
        public override Expression<Func<Voucher, bool>> ToExpression()
        {
            return voucher => voucher.Quantidade > 0;
        }
    }

    public class VoucherAtivoSpecification : Specification<Voucher>
    {
        public override Expression<Func<Voucher, bool>> ToExpression()
        {
            return voucher => voucher.Ativo && voucher.Utilizado == false;
        }
    }

    public class VoucherValidation : SpecValidator<Voucher>
    {
        public VoucherValidation()
        {
            var dataSpec = new VoucherDataSpecification();
            var qtdeSpec = new VoucherQuantidadeSpecification();
            var ativoSpec = new VoucherAtivoSpecification();

            Add("dataSpec", new Rule<Voucher>(dataSpec, "Este voucher está expirado"));
            Add("qtdeSpec", new Rule<Voucher>(qtdeSpec, "Este voucher já foi utilizado"));
            Add("ativoSpec", new Rule<Voucher>(ativoSpec, "Este voucher não está mais ativo"));
        }
    }
}
